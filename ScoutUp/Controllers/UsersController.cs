using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ScoutUp.DAL;
using ScoutUp.Models;
using ScoutUp.Classes;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;
using System.Security.Cryptography;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using ScoutUp.Providers;
using ScoutUp.ViewModels;

namespace ScoutUp.Controllers
{
    public class UsersController : Controller
    {
        private readonly ScoutUpDB _db = new ScoutUpDB();
        private const string LocalLoginProvider = "Local";
        private AppUserManager _userManager;

        public UsersController()
        {
        }

        public UsersController(AppUserManager userManager,
            ISecureDataFormat<AuthenticationTicket> accessTokenFormat)
        {
            UserManager = userManager;
            AccessTokenFormat = accessTokenFormat;
        }

        public AppUserManager UserManager
        {
            get
            {
                return _userManager ?? Request.GetOwinContext().GetUserManager<AppUserManager>();

            }
            private set
            {
                _userManager = value;
            }
        }
        private IAuthenticationManager Authentication
        {
            get { return Request.GetOwinContext().Authentication; }
        }

        public ISecureDataFormat<AuthenticationTicket> AccessTokenFormat { get; private set; }

        #region timeline index about album followers interests
        [System.Web.Mvc.Authorize]
        public ActionResult Index(string id)
        {
            var currentUser = GetUser();
            User user =String.IsNullOrEmpty(id) || currentUser.Id == id ?  GetUser() :  _db.Users.Find(id);
            var targetUser = !(id == null || GetUser().Id == id);

            if (user == null)
                return HttpNotFound();

            if (targetUser)
                ViewBag.Following = IsFollowing(user);

            ViewBag.followerCount = FollowerCount(id);
            ViewBag.currentUser= currentUser;
            ViewBag.targetUser = targetUser;
            return View(user);
        }
        [System.Web.Mvc.Authorize]
        public ActionResult About(string id)
        {
            User user = String.IsNullOrEmpty(id) || GetUser().Id == id ? GetUser() : _db.Users.Find(id);
            var targetUser = !(id == null || GetUser().Id == id);
            if (user == null)
                return HttpNotFound();
            if (targetUser)
                ViewBag.Following = IsFollowing(user);
            ViewBag.followerCount = FollowerCount(id);
            return View(user);
        }
        [System.Web.Mvc.Authorize]
        public ActionResult Followers(string id)
        {
            var currentUser = GetUser();
            User user = String.IsNullOrEmpty(id) || currentUser.Id == id ? currentUser : _db.Users.Find(id);
            var targetUser = !(id == null || currentUser.Id == id);
            if (user == null)
                return HttpNotFound();
            List<FollowingUsers> followList = new List<FollowingUsers>();
            List<UserFollow> followerUsers = _db.UserFollow.Where(e => e.UserBeingFollowedUserId == user.Id).ToList();
            foreach (var item in followerUsers)
            {
                if (item.UserId == currentUser.Id)
                {//Ben onu takip ediyorum o beni takip ediyor mu ?
                    FollowingUsers temp = new FollowingUsers(item.UserId, item.User.UserFirstName + " " + item.User.UserSurname, IsFollowingReverse(user), item.User.UserProfilePhoto);
                    followList.Add(temp);
                }
                else
                {//şu an ki kullanıcı kendisini takip eden kullanıcıyı takip ediyor mu?
                    FollowingUsers temp = new FollowingUsers(item.UserId, item.User.UserFirstName + " " + item.User.UserSurname, IsFollowing(item.User), item.User.UserProfilePhoto);
                    followList.Add(temp);
                }
            }
            if (targetUser)
            {
                ViewBag.Following = IsFollowing(user);
            }
            ViewBag.email = currentUser.Email;
            ViewBag.userid = currentUser.Id;
            ViewBag.Model = followList;
            return View(user);
        }
        [System.Web.Mvc.Authorize]
        public ActionResult FollowSuggest()
        {
            var currentUser = GetUser();
            var suggest = new Suggest();
            var viewModel = suggest.FollowSuggest(currentUser.Id, _db);
            return PartialView("rightside",viewModel);
        }
        [System.Web.Mvc.Authorize]
        public ActionResult Album(string id)
        {
            User user = String.IsNullOrEmpty(id) || GetUser().Id == id ? GetUser() : _db.Users.Find(id);
            var targetUser = !(id == null || GetUser().Id == id);
            if (user == null)
                return HttpNotFound();
            if (targetUser)
                ViewBag.Following = IsFollowing(user);
            ViewBag.followerCount = FollowerCount(id);
            ViewBag.targetUser = targetUser;
            return View(user);
        }

        public ActionResult Interests(string id)
        {
            User user = String.IsNullOrEmpty(id) || GetUser().Id == id ? GetUser() : _db.Users.Find(id);
            var targetUser = !(id == null || GetUser().Id == id);
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
            int rowCount = _db.UserFollow.Where(e => e.UserId == user.Id)
                                         .Count(e => e.UserBeingFollowedUserId == targetUser.Id);
            return rowCount == 1;
        }
        private bool IsFollowingReverse(User targetUser)
        {
            User user = GetUser();
            int rowCount = _db.UserFollow.Where(e => e.UserId == targetUser.Id)
                                         .Count(e => e.UserBeingFollowedUserId == user.Id);
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
        [System.Web.Mvc.HttpPost]
        public ActionResult Login(string loginEmail, string loginPassword,string returnUrl)
        {
            if (string.IsNullOrEmpty(loginEmail) || string.IsNullOrEmpty(loginPassword))
            {
                ViewBag.message = "Kullanıcı adı veya şifre boş bırakılamaz";
                return RedirectToAction("Index", "Home");
            }
            Task<User> userTask = UserManager.FindAsync(loginEmail, loginPassword);
            var user = userTask.Result;
            if (user != null)
            {
                HttpContext.GetOwinContext().Authentication.SignOut();
                var ident = new ClaimsIdentity(
              new[] { 
                 // adding following 2 claim just for supporting default antiforgery provider
                 new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                 new Claim("http://schemas.microsoft.com/accesscontrolservice/2010/07/claims/identityprovider", "ASP.NET Identity", "http://www.w3.org/2001/XMLSchema#string"),

                 new Claim(ClaimTypes.Name,loginEmail),
                  new Claim(ClaimTypes.PrimarySid,user.Id.ToString()),
                 // optionally you could add roles if any
                 new Claim(ClaimTypes.Role, "User"),
              },
              DefaultAuthenticationTypes.ApplicationCookie);

                HttpContext.GetOwinContext().Authentication.SignIn(
                   new AuthenticationProperties { IsPersistent = true }, ident);
                string decodedUrl = "";
                if (!string.IsNullOrEmpty(returnUrl))
                    decodedUrl = Server.UrlDecode(returnUrl.Replace("ReturnUrl=", ""));
                if (user.IsFirstLogin)
                {
                    var newObjUser = _db.Users.Find(user.Id);
                    _db.Users.Attach(newObjUser);
                    newObjUser.IsFirstLogin = false;
                    _db.Entry(newObjUser).Property(e => e.IsFirstLogin).IsModified = true;

                    _db.SaveChanges();
                    return RedirectToAction("InterestCategories", "Users");
                }
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

        [System.Web.Mvc.OverrideAuthentication]
        [HostAuthentication(DefaultAuthenticationTypes.ExternalCookie)]
        [System.Web.Mvc.AllowAnonymous]
        [System.Web.Mvc.Route("ExternalLogin", Name = "ExternalLogin")]
        public async Task<ActionResult> GetExternalLogin(string provider, string error = null)
        {
            if (error != null)
            {
                return RedirectToAction("Index", "Home");
            }

            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            ExternalLoginData externalLogin = ExternalLoginData.FromIdentity(User.Identity as ClaimsIdentity);

            if (externalLogin == null)
            {
                return RedirectToAction("Index", "Home");
            }

            if (externalLogin.LoginProvider != provider)
            {
                Authentication.SignOut(DefaultAuthenticationTypes.ExternalCookie);
                return RedirectToAction("Index", "Home");
            }

            var user = await UserManager.FindAsync(new UserLoginInfo(externalLogin.LoginProvider,
                externalLogin.ProviderKey));

            bool hasRegistered = user != null;

            if (hasRegistered)
            {
                Authentication.SignOut(DefaultAuthenticationTypes.ExternalCookie);

                ClaimsIdentity oAuthIdentity = await user.GenerateUserIdentityAsync(UserManager,
                   OAuthDefaults.AuthenticationType);
                ClaimsIdentity cookieIdentity = await user.GenerateUserIdentityAsync(UserManager,
                    CookieAuthenticationDefaults.AuthenticationType);

                AuthenticationProperties properties = ApplicationOAuthProvider.CreateProperties(user.UserFirstName);
                Authentication.SignIn(properties, oAuthIdentity, cookieIdentity);
            }
            else
            {
                IEnumerable<Claim> claims = externalLogin.GetClaims();
                ClaimsIdentity identity = new ClaimsIdentity(claims, OAuthDefaults.AuthenticationType);
                Authentication.SignIn(identity);
            }

            return RedirectToAction("Index", "Home");
        }

        public ActionResult InterestCategories()
        {
            User user = GetUser();
            return View(user);
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
        [System.Web.Mvc.HttpPost]
        public async Task<ActionResult> Create(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("",
                    "Unable to save changes. Try again, and if the problem persists see your system administrator.");
                var modelErrors = new List<string>();
                foreach (System.Web.Mvc.ModelState modelState in ViewData.ModelState.Values)
                {
                    foreach (System.Web.Mvc.ModelError error in modelState.Errors)
                    {
                        modelErrors.Add(error.ErrorMessage);
                    }
                }
                return Json(new LoginResult(0, "Model state hatası", null, modelErrors));
            }

            try
            {
                var result = await UserManager.CreateAsync(new Models.User()
                {
                    UserName = model.UserName, Email = model.Email,UserFirstName =model.UserFirstName, UserSurname = model.UserSurname,UserBirthDate = model.UserBirthDate,
                    UserCity = model.UserCity,UserAbout = model.UserAbout,UserGender = UserGender.Erkek,IsFirstLogin = model.IsFirstLogin
                }, model.Password);

                if (!result.Succeeded)
                {
                    ModelState.AddModelError("",
                        "Unable to save changes. Try again, and if the problem persists see your system administrator.");
                    return Json(new LoginResult(0,"", result.Errors));
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("",
                    "Unable to save changes. Try again, and if the problem persists see your system administrator.");
                return Json(new LoginResult(0,ex.Message));
                throw;
            }
            return Json(new LoginResult(1));
        }

    
    #endregion
    #region edit profile hobbies update photo
    /// <summary>
    /// Profil update edildilten sonra ve ilk girişte çalışır id parametresi verimediği zaman zaten giriş yapmış olması gerektiği için sessiondan id parametresi alınarak devam edilir.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [System.Web.Mvc.Authorize]
        public ActionResult EditProfileBasic()
        {
            Models.User user = GetUser();
            
            if (user == null)
            {
                return HttpNotFound();
            }
            ViewBag.followerCount = FollowerCount(user.Id);
            return View(user);
        }
        /// <summary>
        /// Kullanıcı profilinin editleme kısmı sadece editlenen kısımlar değişir gerisi kalır entry.Property bu işe yarıyor.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [System.Web.Mvc.Authorize]
        [System.Web.Mvc.HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditProfileBasic([Bind(Include = "Id,UserFirstName,UserSurname,Email,UserCity,UserBirthDate,UserGender,UserAbout,UserProfilePhoto")]ScoutUp.Models.User user)
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
                entry.Property(e => e.UserFirstName).IsModified = true;
                entry.Property(e => e.Email).IsModified = true;
                entry.Property(e => e.UserSurname).IsModified = true;
                entry.Property(e => e.UserBirthDate).IsModified = true;
                entry.Property(e => e.UserCity).IsModified = true;
                entry.Property(e => e.UserGender).IsModified = true;
                entry.Property(e => e.UserAbout).IsModified = true;
                entry.Property(e => e.Id).IsModified = false;
                entry.Property(e => e.SecurityStamp).IsModified = false;
                entry.Property(e => e.IsFirstLogin).IsModified = false;
                entry.Property(e => e.UserName).IsModified = false;
                entry.Property(e => e.TwoFactorEnabled).IsModified = false;
                entry.Property(e => e.PasswordHash).IsModified = false;
                entry.Property(e => e.PhoneNumber).IsModified = false;
                entry.Property(e => e.LockoutEnabled).IsModified = false;
                entry.Property(e => e.LockoutEndDateUtc).IsModified = false;
                entry.Property(e => e.AccessFailedCount).IsModified = false;
                entry.Property(e => e.UserProfilePhoto).IsModified = flag;
                
                _db.SaveChanges();
                return View(user);
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest,ex.Message);
            }
        }
        /// <summary>
        /// News feed ten profil fotoğrafı ekler aynı anda albüme de ekler.
        /// Fotoğraf boyutlarını 190x190 ve 640x640 olarak /images/post-images içine kaydeler.
        /// </summary>
        /// <returns></returns>
        [System.Web.Mvc.HttpPost]
        [System.Web.Mvc.Authorize]
        public ActionResult UpdateProfilePhoto()
        {
            var user = GetUser();
            var photo = new UserPhotos();
            var databasePathSmall = "";
            var databasePathBig = "";
            var flag = false;
            var resizer= new ImageResize();
            var tracer = "";
            try
            {
                if (Request.Files.Count > 0)
                {
                    var file = Request.Files[0];

                    if (file != null && file.ContentLength > 0)
                    {
                        Image imgSmall = resizer.RezizeImage(Image.FromStream(file.InputStream, true, true), 190, 190);
                        Image imgBig = resizer.RezizeImage(Image.FromStream(file.InputStream, true, true), 640, 640);
                        var fileName = Path.GetFileName(file.FileName);
                        var fileNameSmall = fileName.Replace(".", "-tumbnail.");
                        var fileNameBig = fileName.Replace(".", "-big.");
                        var pathSmall = Path.Combine(Server.MapPath("~/images/post-images"), fileNameSmall);
                       var pathBig = Path.Combine(Server.MapPath("~/images/post-images"), fileNameBig);
                        databasePathSmall = "../../images/post-images/" + fileNameSmall;
                        databasePathBig = "../../images/post-images/" + fileNameBig;
                        tracer += pathSmall + " " + pathBig;
                        imgSmall.Save(pathSmall);
                        imgBig.Save(pathBig);
                        flag = true;
                    }
                }
            }
            catch (Exception ex)
            {
                return Json(new LoginResult(result:0,error:ex.Message + " filename="+ tracer));
            }
            user.UserProfilePhoto = databasePathSmall;
            _db.Users.Attach(user);
            photo.UserId = user.Id;
            photo.UserPhotoSmall = databasePathSmall;
            photo.UserPhotoBig = databasePathBig;
            _db.UserPhotos.Add(photo);
            _db.UsersLastMoves.Add(new UsersLastMoves { MoveDate = DateTime.Now, UserId = user.Id, UsersLastMoveText =" profil fotoğrafını güncelledi.", UsersMoveLink = "/users/album/" + user.Id });
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
        [System.Web.Mvc.Authorize]
        public ActionResult UpdateProfilePhoto(int? id)
        {
            if(id ==null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var user = GetUser();
            var photo = user.UserPhotos.FirstOrDefault(e => e.UserPhotosID == id);
            if (photo == null) { return new HttpStatusCodeResult(HttpStatusCode.BadRequest); }
            user.UserProfilePhoto = photo.UserPhotoSmall;
            _db.UsersLastMoves.Add(new UsersLastMoves { MoveDate = DateTime.Now, UserId = user.Id, UsersLastMoveText =" profil fotoğrafını güncelledi.", UsersMoveLink = "/users/album/" + user.Id });
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
        [System.Web.Mvc.Authorize]
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
        [System.Web.Mvc.Authorize]
        public ActionResult All()
        {
            var t = HttpContext.GetOwinContext().Authentication.User.Claims;
            string userId = "";
            foreach (var item in t)
            {
                if (item.Type.Contains("primarysid"))
                    userId = (item.Value);

            }
            User user = _db.Users.FirstOrDefault(e => e.Id == userId);
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
        [System.Web.Mvc.Authorize]
        public ActionResult EditProfileInterests()
        {
            User user = GetUser();
            ViewBag.followerCount = FollowerCount(user.Id);
            return View(user);
        }
        /// <summary>
        /// Kullanıcı yeni hobi eklemek istediğinde burası çalışır 1 veya daha fazla hobi seçip gönderebilir. daha önce seçtiği hobiyi seçemediği için bu durum sorun olmuyor.
        /// </summary>
        /// <param name="hobbiesName"></param>
        /// <returns></returns>
        [System.Web.Mvc.Authorize]
        [System.Web.Mvc.HttpPost]
        public ActionResult EditProfileInterests(string hobbiesName)
        {
            string[] split = hobbiesName.Split(',');
            List<UserHobbies> userHobbies = new List<UserHobbies>();
            List<Hobbies> hobbies = split.Select(item => _db.Hobbies.FirstOrDefault(e => e.HobbiesName == item)).ToList();
            var t = HttpContext.GetOwinContext().Authentication.User.Claims;
            string userId = "";
            foreach (var item in t)
            {
                if (item.Type.Contains("primarysid"))
                    userId = item.Value;

            }
            foreach (var item in hobbies)
            {
                userHobbies.Add(new UserHobbies { UserId = userId, HobbiesID = item.HobbiesID });
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
        [System.Web.Mvc.Authorize]
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
        [System.Web.Mvc.Authorize]
        public ActionResult Follow(string id)
        {
            if(id==null)
            {
                return new HttpStatusCodeResult( HttpStatusCode.BadRequest);
            }
            try
            {
                User user = GetUser();
                User otherUser = _db.Users.Find(id);
                _db.UserFollow.Add(new UserFollow { UserId = user.Id, UserBeingFollowedUserId = id, IsFollowing = true });
                _db.UsersLastMoves.Add(new UsersLastMoves {MoveDate = DateTime.Now,UserId = user.Id,UsersLastMoveText =" "+ otherUser.UserFirstName+" "+otherUser.UserSurname+"'i takip etti.",UsersMoveLink = "/users/index/"+otherUser.Id});
                _db.SaveChanges();
                return Json(new LoginResult(1),JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(new LoginResult(0),JsonRequestBehavior.AllowGet);
            }
        }
        #endregion
        #region Friends
        /// <summary>
        /// Kullanıcının takip ettiği kullanıcıları gösterir
        /// </summary>
        /// <returns>/users/friends</returns>
        //id eklemem lazım buraya following için
        [System.Web.Mvc.Authorize]
        public ActionResult Friends(string id)
        {
            User user = GetUser();
            if (user == null)
            {
                return HttpNotFound();
            }
            List<FollowingUsers> followList = user.UserFollow.Select(item => _db.Users.Find(item.UserBeingFollowedUserId)).Select(temp => new FollowingUsers(temp.Id, temp.UserFirstName + " " + temp.UserSurname,temp.UserProfilePhoto)).ToList();
            ViewBag.email = user.Email;
            ViewBag.followList = followList;
            return View(user);
        }
        [System.Web.Mvc.Authorize]
        public ActionResult Nearby()
        {
            User user = GetUser();
            if (user == null)
            {
                return HttpNotFound();
            }
            var suggest =new Suggest();
            var list = suggest.FollowSuggest(user.Id, _db, true);
            return View(user);
        }
        [System.Web.Mvc.Authorize]
        public ActionResult NearbyUsers()
        {
            User user = GetUser();
            if (user == null)
            {
                return HttpNotFound();
            }
            var suggest = new Suggest();
            var list = suggest.FollowSuggest(user.Id, _db, true);
            return PartialView("NearbyUsers",list);
        }
        [System.Web.Mvc.Authorize]
        public ActionResult StopFollow(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = GetUser();
            UserFollow removeFollow = _db.UserFollow.Where(e => e.UserId==user.Id).FirstOrDefault(e => e.UserBeingFollowedUserId==id);
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
        [System.Web.Mvc.Authorize]
        public int FollowerCount(string id)
        {
            User user = String.IsNullOrEmpty(id) ? GetUser() : _db.Users.Find(id);
            List<UserFollow> followerUsers = _db.UserFollow.Where(e => e.UserBeingFollowedUserId == user.Id).ToList();
            return followerUsers.Count;
        }
        /// <summary>
        /// Right-side kısmından kullanıcı birini takip ettiğinde friends partial kısmını günceller sayfa yenilenmeden yeni takip edilen kişi alana eklenir.
        /// </summary>
        /// <returns></returns>
        [System.Web.Mvc.Authorize]
        public ActionResult FriendsPartial()
        {
            User user = GetUser();
            if (user == null)
            {
                return HttpNotFound();
            }
            List<FollowingUsers> followList = user.UserFollow.Select(item => _db.Users.Find(item.UserBeingFollowedUserId))
                                                            .Select(temp => new FollowingUsers(temp.Id, temp.UserFirstName + " " + temp.UserSurname,temp.UserProfilePhoto))
                                                            .ToList();
            ViewBag.followList = followList;
            return PartialView("friendsLoop", user);
        }
        #endregion
        #region Rating 
        [System.Web.Mvc.HttpPost]
        public ActionResult AddRating(RatingViewModel model)
        {
            if (!ModelState.IsValid) { return Json(new { success = false }); }
            var userid =HttpContext.GetOwinContext().Authentication.User.Identity.GetUserId();
            var userRating = new UserRatings { UserId = userid,CategoryItemID =model.ItemId,UserRating =Convert.ToDouble( model.Rating)};
            var ratedBefore = _db.UserRatings.Where(e => e.UserId == userid).FirstOrDefault(i => i.CategoryItemID == model.ItemId);
            if (ratedBefore != null)
                _db.UserRatings.Remove(ratedBefore);

            try
            {
                _db.UserRatings.Add(userRating);
                _db.UsersLastMoves.Add(new UsersLastMoves { MoveDate = DateTime.Now, UserId = userRating.UserId, UsersLastMoveText = " bir öğeyi değerlendirdi.", UsersMoveLink = "/users/interests/" + userRating.UserId });
                _db.SaveChanges();
                return Json(new {success = true});
            }
            catch (Exception)
            {
                return Json(new {success = false});
            }
           
        }
        #endregion

        public ActionResult UsersLastMoves(string id)
        {
            User user = string.IsNullOrEmpty(id) || GetUser().Id == id ? GetUser() : _db.Users.Find(id);
            ViewBag.userName = user.UserFirstName;
            var userMoveViewModels = _db.UsersLastMoves.Where(e => e.UserId == user.Id).Select(e => new UserMoveViewModel {MoveDate = e.MoveDate,UsersLastMoveText = e.UsersLastMoveText,UsersMoveLink = e.UsersMoveLink}).OrderByDescending(e=> e.MoveDate).Take(5).ToList();
            return PartialView("StickySideBar", userMoveViewModels);
        }
        public ActionResult Messages(string id)
        {
            if (!String.IsNullOrEmpty(id))
                ViewBag.targetUserId = id;
            else
                ViewBag.targetUserId = "";

            var user = GetUser();
            List<FollowingUsers> followList = user.UserFollow.Select(item => _db.Users.Find(item.UserBeingFollowedUserId)).Select(temp => new FollowingUsers(temp.Id, temp.UserFirstName + " " + temp.UserSurname, temp.UserProfilePhoto)).ToList();
            ViewBag.email = user.Email;
            ViewBag.followList = followList;
            ViewBag.currentUser = user;
            return View(user);
        }
        public User GetUser()
        {
            var t = HttpContext.GetOwinContext().Authentication;
            var id = t.User.Identity.GetUserId();
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

    public class ExternalLoginData
    {
        public string LoginProvider { get; set; }
        public string ProviderKey { get; set; }
        public string UserName { get; set; }

        public IList<Claim> GetClaims()
        {
            IList<Claim> claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.NameIdentifier, ProviderKey, null, LoginProvider));

            if (UserName != null)
            {
                claims.Add(new Claim(ClaimTypes.Name, UserName, null, LoginProvider));
            }

            return claims;
        }

        public static ExternalLoginData FromIdentity(ClaimsIdentity identity)
        {
            if (identity == null)
            {
                return null;
            }

            Claim providerKeyClaim = identity.FindFirst(ClaimTypes.NameIdentifier);

            if (providerKeyClaim == null || String.IsNullOrEmpty(providerKeyClaim.Issuer)
                                         || String.IsNullOrEmpty(providerKeyClaim.Value))
            {
                return null;
            }

            if (providerKeyClaim.Issuer == ClaimsIdentity.DefaultIssuer)
            {
                return null;
            }

            return new ExternalLoginData
            {
                LoginProvider = providerKeyClaim.Issuer,
                ProviderKey = providerKeyClaim.Value,
                UserName = identity.FindFirstValue(ClaimTypes.Name)
            };
        }
    }
    public static class RandomOAuthStateGenerator
    {
        private static RandomNumberGenerator _random = new RNGCryptoServiceProvider();

        public static string Generate(int strengthInBits)
        {
            const int bitsPerByte = 8;

            if (strengthInBits % bitsPerByte != 0)
            {
                throw new ArgumentException("strengthInBits must be evenly divisible by 8.", "strengthInBits");
            }

            int strengthInBytes = strengthInBits / bitsPerByte;

            byte[] data = new byte[strengthInBytes];
            _random.GetBytes(data);
            return HttpServerUtility.UrlTokenEncode(data);
        }
    }


}



