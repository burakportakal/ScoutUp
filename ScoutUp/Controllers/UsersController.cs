using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Configuration;
using System.Web;
using System.Web.Mvc;
using ScoutUp.DAL;
using ScoutUp.Models;
using ScoutUp.Classes;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.SignalR.Infrastructure;
using Microsoft.Owin.Security;
using ScoutUp.ViewModels;

namespace ScoutUp.Controllers
{
    public class UsersController : Controller
    {
        private readonly ScoutUpDB _db = new ScoutUpDB();
        #region timeline index about album followers
        [Authorize]
        public ActionResult Index(int? id)
        {
            var currentUser = GetUser();
            User user = id == null || currentUser.UserID == id ?  GetUser() :  _db.Users.Find(id);
            var targetUser = !(id == null || GetUser().UserID == id);

            if (user == null)
                return HttpNotFound();

            if (targetUser)
                ViewBag.Following = IsFollowing(user);

            ViewBag.followerCount = FollowerCount(id);
            ViewBag.currentUser= currentUser;
            ViewBag.targetUser = targetUser;
            return View(user);
        }
        [Authorize]
        public ActionResult About(int? id)
        {
            User user = id == null || GetUser().UserID == id ? GetUser() : _db.Users.Find(id);
            var targetUser = !(id == null || GetUser().UserID == id);
            if (user == null)
                return HttpNotFound();
            if (targetUser)
                ViewBag.Following = IsFollowing(user);
            ViewBag.followerCount = FollowerCount(id);
            return View(user);
        }
        [Authorize]
        public ActionResult Followers(int? id)
        {
            var currentUser = GetUser();
            User user = id == null || currentUser.UserID == id ? currentUser : _db.Users.Find(id);
            var targetUser = !(id == null || currentUser.UserID == id);
            if (user == null)
                return HttpNotFound();
            List<FollowingUsers> followList = new List<FollowingUsers>();
            List<UserFollow> followerUsers = _db.UserFollow.Where(e => e.UserBeingFollowedUserID == user.UserID).ToList();
            foreach (var item in followerUsers)
            {
                if (item.UserID == currentUser.UserID)
                {//Ben onu takip ediyorum o beni takip ediyor mu ?
                    FollowingUsers temp = new FollowingUsers(item.UserID, item.User.UserName + " " + item.User.UserSurname, IsFollowingReverse(user), item.User.UserProfilePhoto);
                    followList.Add(temp);
                }
                else
                {//şu an ki kullanıcı kendisini takip eden kullanıcıyı takip ediyor mu?
                    FollowingUsers temp = new FollowingUsers(item.UserID, item.User.UserName + " " + item.User.UserSurname, IsFollowing(item.User), item.User.UserProfilePhoto);
                    followList.Add(temp);
                }
            }
            if (targetUser)
            {
                ViewBag.Following = IsFollowing(user);
            }
            ViewBag.email = currentUser.UserEmail;
            ViewBag.userid = currentUser.UserID;
            ViewBag.Model = followList;
            return View(user);
        }
        [Authorize]
        public ActionResult FollowSuggest()
        {
            var currentUser = GetUser();
            var suggest = new Suggest();
            var viewModel = suggest.FollowSuggest(currentUser.UserID, _db);
            return PartialView("rightside",viewModel);
        }
        [Authorize]
        public ActionResult Album(int? id)
        {
            User user = id == null || GetUser().UserID == id ? GetUser() : _db.Users.Find(id);
            var targetUser = !(id == null || GetUser().UserID == id);
            if (user == null)
                return HttpNotFound();
            if (targetUser)
                ViewBag.Following = IsFollowing(user);
            ViewBag.followerCount = FollowerCount(id);
            ViewBag.targetUser = targetUser;
            return View(user);
        }
        private bool IsFollowing(User targetUser)
        {
            User user = GetUser();
            int rowCount = _db.UserFollow.Where(e => e.UserID == user.UserID)
                                         .Count(e => e.UserBeingFollowedUserID == targetUser.UserID);
            return rowCount == 1;
        }
        private bool IsFollowingReverse(User targetUser)
        {
            User user = GetUser();
            int rowCount = _db.UserFollow.Where(e => e.UserID == targetUser.UserID)
                                         .Count(e => e.UserBeingFollowedUserID == user.UserID);
            return rowCount == 1;
        }
        #endregion
        #region login logout create
        /// <summary>
        /// Kullanıcı Login kısmı 
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Login(string email, string password,string returnUrl)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                ViewBag.message = "Kullanıcı adı veya şifre boş bırakılamaz";
                return RedirectToAction("Index","Home");
            }
            
            User user = _db.Users.Where(e => e.UserEmail == email).FirstOrDefault(p => p.UserPassword == password);
            if (user != null)
            {
                PrincipalUserIdProvider asd= new PrincipalUserIdProvider();
                ClaimsPrincipal pr = new ClaimsPrincipal();

                HttpContext.GetOwinContext().Authentication.SignOut();
                Session["id"] = user.UserID;
                   var ident = new ClaimsIdentity(
                 new[] { 
                 // adding following 2 claim just for supporting default antiforgery provider
                 new Claim(ClaimTypes.NameIdentifier, user.UserID.ToString()),
                 new Claim("http://schemas.microsoft.com/accesscontrolservice/2010/07/claims/identityprovider", "ASP.NET Identity", "http://www.w3.org/2001/XMLSchema#string"),

                 new Claim(ClaimTypes.Name,email),
                  new Claim(ClaimTypes.PrimarySid,user.UserID.ToString()),
                 // optionally you could add roles if any
                 new Claim(ClaimTypes.Role, "User"),
                 },
                 DefaultAuthenticationTypes.ApplicationCookie);
                
                   HttpContext.GetOwinContext().Authentication.SignIn(
                      new AuthenticationProperties { IsPersistent = true }, ident);
                string decodedUrl = "";
                if (!string.IsNullOrEmpty(returnUrl))
                    decodedUrl = Server.UrlDecode(returnUrl.Replace("ReturnUrl=",""));

                if (Url.IsLocalUrl(decodedUrl))
                    return Redirect(decodedUrl);
                else
                    return RedirectToAction("Newsfeed", "Home");
            }
           else 
            {
                ViewBag.message = "Kullanıcı adı veya şifre boş bırakılamaz";
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult Edit()
        {
            return View();
        }
        public ActionResult Logout()
        {
            HttpContext.GetOwinContext().Authentication.SignOut();
            return RedirectToAction("index", "Home");
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        /// Kullanıcı register bölümü LoginResult olduğuna bakma sadece kaydolup kaydolmadığını anlamak için.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserName,UserSurname,UserPassword,UserEmail,UserCity,UserBirthDate,UserGender,UserAbout")] ScoutUp.Models. User user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var usedBefore = _db.Users.FirstOrDefault(e => e.UserEmail == user.UserEmail);
                    if (usedBefore != null) return Json("Bu email daha önce kullanılmış");
                    _db.Users.Add(user);
                    _db.SaveChanges();
                    return Json(new LoginResult(1));
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }

            return Json(new LoginResult(0));
        }
        #endregion
        #region edit profile hobbies update photo
        /// <summary>
        /// Profil update edildilten sonra ve ilk girişte çalışır id parametresi verimediği zaman zaten giriş yapmış olması gerektiği için sessiondan id parametresi alınarak devam edilir.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize]
        public ActionResult EditProfileBasic()
        {
            Models.User user = GetUser();
            
            if (user == null)
            {
                return HttpNotFound();
            }
            ViewBag.followerCount = FollowerCount(user.UserID);
            return View(user);
        }
        /// <summary>
        /// Kullanıcı profilinin editleme kısmı sadece editlenen kısımlar değişir gerisi kalır entry.Property bu işe yarıyor.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditProfileBasic([Bind(Include = "UserID,UserName,UserSurname,UserEmail,UserCity,UserBirthDate,UserGender,UserAbout,UserProfilePhoto")]ScoutUp.Models.User user)
        {
            if (!ModelState.IsValid) return new HttpStatusCodeResult(HttpStatusCode.BadRequest); ;
            var databasePath = "";
            var flag = false;
            if (Request.Files.Count > 0)
            {
                var file = Request.Files[0];

                if (file != null && file.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    var path = Path.Combine(Server.MapPath("~/images/post-images"), fileName);
                    databasePath = "../../images/post-images/" + fileName;
                    file.SaveAs(path);
                    flag = true;
                }
            }
            user.UserProfilePhoto = databasePath;
            try
            {
                _db.Users.Attach(user);
                var entry = _db.Entry(user);
                entry.Property(e => e.UserName).IsModified = true;
                entry.Property(e => e.UserSurname).IsModified = true;
                entry.Property(e => e.UserBirthDate).IsModified = true;
                entry.Property(e => e.UserCity).IsModified = true;
                entry.Property(e => e.UserGender).IsModified = true;
                entry.Property(e => e.UserAbout).IsModified = true;
                entry.Property(e => e.UserProfilePhoto).IsModified = flag;
                _db.SaveChanges();
                return View(user);
            }
            catch (Exception)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
        }
        /// <summary>
        /// News feed ten profil fotoğrafı ekler aynı anda albüme de ekler.
        /// Fotoğraf boyutlarını 190x190 ve 640x640 olarak /images/post-images içine kaydeler.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public ActionResult UpdateProfilePhoto()
        {
            var user = GetUser();
            var photo = new UserPhotos();
            var databasePathSmall = "";
            var databasePathBig = "";
            var flag = false;
            var resizer= new ImageResize();
            if (Request.Files.Count > 0)
            {
                var file = Request.Files[0];

                if (file != null && file.ContentLength > 0)
                {
                    Image imgSmall =resizer.RezizeImage(Image.FromStream(file.InputStream, true, true), 190, 190);
                    Image imgBig = resizer.RezizeImage(Image.FromStream(file.InputStream, true, true), 640, 640);
                    var fileName = Path.GetFileName(file.FileName);
                    var fileNameSmall=fileName.Replace(".","-tumbnail.");
                    var fileNameBig = fileName.Replace(".", "-big.");
                    var pathSmall = Path.Combine(Server.MapPath("~/images/post-images"), fileNameSmall);
                    var pathBig = Path.Combine(Server.MapPath("~/images/post-images"), fileNameBig);
                    databasePathSmall = "../../images/post-images/" + fileNameSmall;
                    databasePathBig = "../../images/post-images/" + fileNameBig;
                    imgSmall.Save(pathSmall);
                    imgBig.Save(pathBig);
                    flag = true;
                }
            }
            user.UserProfilePhoto = databasePathSmall;
            _db.Users.Attach(user);
            photo.UserID = user.UserID;
            photo.IsDeleted = false;
            photo.UserPhotoSmall = databasePathSmall;
            photo.UserPhotoBig = databasePathBig;
            _db.UserPhotos.Add(photo);
            var entry = _db.Entry(user);
            entry.Property(e => e.UserProfilePhoto).IsModified = flag;
            try
            {
                _db.SaveChanges();
                return Json(1);
            }
            catch (Exception)
            {
                return Json(0);
            }
        }
        /// <summary>
        /// Albümdeki bir fotoğrafı profil fotoğrafı yapar
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize]
        public ActionResult UpdateProfilePhoto(int? id)
        {
            if(id ==null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var user = GetUser();
            var photo = user.UserPhotos.FirstOrDefault(e => e.UserPhotosID == id);
            if (photo == null) { return new HttpStatusCodeResult(HttpStatusCode.BadRequest); }
            user.UserProfilePhoto = photo.UserPhotoSmall;
            _db.Users.Attach(user);
             var entry = _db.Entry(user);
             entry.Property(e => e.UserProfilePhoto).IsModified = true;
            try
            {
                _db.SaveChanges();
                return Json(1, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
        }
        /// <summary>
        /// Albümden fotoğrafı sil
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize]
        public ActionResult deletePhoto(int? id)
        {
            if(id ==null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var user = GetUser();
            var photo = user.UserPhotos.FirstOrDefault(e => e.UserPhotosID == id);
            var isProfilePhoto = photo != null && user.UserProfilePhoto == photo.UserPhotoSmall;
            if (isProfilePhoto)
            {
                user.UserProfilePhoto = "";
                _db.Users.Attach(user);
                var entry = _db.Entry(user);
                entry.Property(e => e.UserProfilePhoto).IsModified = true;
            }

            if (photo != null) _db.UserPhotos.Remove(photo); else { return new HttpStatusCodeResult(HttpStatusCode.BadRequest); }
            try
            {
                _db.SaveChanges();
                return Json(1,JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(0,JsonRequestBehavior.AllowGet);
            }
        }
        /// <summary>
        /// Sayfa yüklenirken kullanıcının seçmediği hobbileri oto komplete gönderir
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public ActionResult All()
        {
            var t = HttpContext.GetOwinContext().Authentication.User.Claims;
            int userId = 0;
            foreach (var item in t)
            {
                if (item.Type.Contains("primarysid"))
                    userId = Convert.ToInt32(item.Value);

            }
            User user = _db.Users.FirstOrDefault(e => e.UserID == userId);
            List<Hobbies> userHobbies = _db.Hobbies.ToList();
            foreach (var item in user.UserHobbies)
            {
                userHobbies.Remove(item.Hobbies);
            }
            return Json(userHobbies, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// sayfa yüklenirken çalışan edit sayfası userı bulur gönderir burdan gelen veriyle seçtiği hobileri yeşil kutularda gösterir içerisinde userHobbiesid bulunduğu için silmesi kolay
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public ActionResult EditProfileInterests()
        {
            User user = GetUser();
            ViewBag.followerCount = FollowerCount(user.UserID);
            return View(user);
        }
        /// <summary>
        /// Kullanıcı yeni hobi eklemek istediğinde burası çalışır 1 veya daha fazla hobi seçip gönderebilir. daha önce seçtiği hobiyi seçemediği için bu durum sorun olmuyor.
        /// </summary>
        /// <param name="hobbiesName"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        public ActionResult EditProfileInterests(string hobbiesName)
        {
            string[] split = hobbiesName.Split(',');
            List<UserHobbies> userHobbies = new List<UserHobbies>();
            List<Hobbies> hobbies = split.Select(item => _db.Hobbies.FirstOrDefault(e => e.HobbiesName == item)).ToList();
            var t = HttpContext.GetOwinContext().Authentication.User.Claims;
            int userId = 0;
            foreach (var item in t)
            {
                if (item.Type.Contains("primarysid"))
                    userId = Convert.ToInt32(item.Value);

            }
            foreach (var item in hobbies)
            {
                userHobbies.Add(new UserHobbies { UserID = userId, HobbiesID = item.HobbiesID });
            }
            try
            {
                _db.UserHobbies.AddRange(userHobbies);
                _db.SaveChanges();
            }
            catch (Exception)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return RedirectToAction("EditProfileInterests");
        }
        /// <summary>
        /// tıklanan hobiyi siler
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: UserHobbies/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserHobbies userHobbies = _db.UserHobbies.Find(id);
            if (userHobbies == null)
            {
                return HttpNotFound();
            }
            _db.UserHobbies.Remove(userHobbies);
            _db.SaveChanges();
            return RedirectToAction("EditProfileInterests");
        }
        #endregion update photo
        #region Follow
        /// <summary>
        /// Bir kullanıcının diğer bir kullanıcıyı takip etmesini sağlar.
        /// </summary>
        /// <param name="id">Takip edilecek kullanıcının idsi.</param>
        /// <returns></returns>
        [Authorize]
        public ActionResult Follow(int? id)
        {
            if(id==null)
            {
                return new HttpStatusCodeResult( HttpStatusCode.BadRequest);
            }
            try
            {
                User user = GetUser();
                _db.UserFollow.Add(new UserFollow { UserID = user.UserID, UserBeingFollowedUserID = (int)id, IsFollowing = true });
                _db.SaveChanges();
                return Json(new LoginResult(1),JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new LoginResult(0),JsonRequestBehavior.AllowGet);
            }
        }
        /// <summary>
        /// Kullanıcıya takip etmesi için öneri sunar Projenin asıl amacı çok geliştirilecek.
        /// </summary>
        /// <returns></returns>
        //[Authorize]
        //public List<UsersToFollow> FollowSuggest()
        //{
        //    User user = GetUser();
        //    if (user == null)
        //    {
        //        return null;
        //    }
        //    if (HttpContext.GetOwinContext().Authentication.User.Identity.Name != user.UserEmail)
        //    {
        //        return null;
        //    }
        //    List<User> allUsers = _db.Users.ToList();
        //    allUsers.Remove(user);
        //    foreach (var item in user.UserFollow)
        //    {
        //        allUsers.Remove(_db.Users.Find(item.UserBeingFollowedUserID));
        //    }
        //    List<UsersToFollow> usersToFollow = new List<UsersToFollow>();
        //    foreach (var item in allUsers)
        //    {
        //        UsersToFollow temp = new UsersToFollow(item.UserID, item.UserName + " " + item.UserSurname,item.UserProfilePhoto);
        //        usersToFollow.Add(temp);
        //    }
        //    return usersToFollow;
        //}
        #endregion
        #region Friends
        /// <summary>
        /// Kullanıcının takip ettiği kullanıcıları gösterir
        /// </summary>
        /// <returns>/users/friends</returns>
        //id eklemem lazım buraya following için
        [Authorize]
        public ActionResult Friends(int? id)
        {
            User user = GetUser();
            if (user == null)
            {
                return HttpNotFound();
            }
            if (HttpContext.GetOwinContext().Authentication.User.Identity.Name != user.UserEmail)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            List<FollowingUsers> followList = user.UserFollow.Select(item => _db.Users.Find(item.UserBeingFollowedUserID)).Select(temp => new FollowingUsers(temp.UserID, temp.UserName + " " + temp.UserSurname,temp.UserProfilePhoto)).ToList();
            ViewBag.email = GetUser().UserEmail;
            ViewBag.followList = followList;
            return View(user);
        }
        [Authorize]
        public ActionResult StopFollow(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = GetUser();
            UserFollow removeFollow = _db.UserFollow.Where(e => e.UserID==user.UserID).FirstOrDefault(e => e.UserBeingFollowedUserID==id);
            if (removeFollow == null)
                return Json(new LoginResult(0), JsonRequestBehavior.AllowGet);
            _db.UserFollow.Remove(removeFollow);
            _db.SaveChanges();
            return Json(new LoginResult(1),JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Dinamik takipçi sayısı. Jquery get ile 5 saniyede 1 takipçi sayısını öğrenmek için istek gelir.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize]
        public int FollowerCount(int? id)
        {
            User user = id == null ? GetUser() : _db.Users.Find(id);
            List<UserFollow> followerUsers = _db.UserFollow.Where(e => e.UserBeingFollowedUserID == user.UserID).ToList();
            return followerUsers.Count;
        }
        /// <summary>
        /// Right-side kısmından kullanıcı birini takip ettiğinde friends partial kısmını günceller sayfa yenilenmeden yeni takip edilen kişi alana eklenir.
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public ActionResult FriendsPartial()
        {
            User user = GetUser();
            if (user == null)
            {
                return HttpNotFound();
            }
            if (HttpContext.GetOwinContext().Authentication.User.Identity.Name != user.UserEmail)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            List<FollowingUsers> followList = user.UserFollow.Select(item => _db.Users.Find(item.UserBeingFollowedUserID))
                                                            .Select(temp => new FollowingUsers(temp.UserID, temp.UserName + " " + temp.UserSurname,temp.UserProfilePhoto))
                                                            .ToList();
            ViewBag.followList = followList;
            return PartialView("friendsLoop", user);
        }
        #endregion
        #region Rating 
        [HttpPost]
        public ActionResult AddRating(RatingViewModel model)
        {
            if (!ModelState.IsValid) { return Json(new { success = false }); }
            var userid =Convert.ToInt32( HttpContext.GetOwinContext().Authentication.User.Identity.GetUserId());
            var userRating = new UserRatings { UserID = userid,CategoryItemID =model.ItemId,UserRating =Convert.ToDouble( model.Rating)};
            var ratedBefore = _db.UserRatings.Where(e => e.UserID == userid).FirstOrDefault(i => i.CategoryItemID == model.ItemId);
            if (ratedBefore != null)
                _db.UserRatings.Remove(ratedBefore);

            try
            {
                _db.UserRatings.Add(userRating);
                _db.SaveChanges();
                return Json(new {success = true});
            }
            catch (Exception)
            {
                return Json(new {success = false});
            }
           
        }
        #endregion
        //Normal Pearson Korelasyonu
        public User GetUser()
        {
            var t = HttpContext.GetOwinContext().Authentication.User.Claims;
            int id = (from item in t where item.Type.Contains("primarysid") select Convert.ToInt32(item.Value)).FirstOrDefault();
                //Extensions classı kullanılıyor
                return _db.UserById(id);
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }
    }

 


}
