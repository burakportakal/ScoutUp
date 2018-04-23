using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ScoutUp.DAL;
using ScoutUp.Models;
using ScoutUp.Classes;
using System.Security.Claims;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;

namespace ScoutUp.Controllers
{
    public class UsersController : Controller
    {
        private ScoutUpDB db = new ScoutUpDB();

        /// <summary>
        /// Kullanıcı Login kısmı 
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Login(string email, string password)
        {
            if (email == null || password==null)
            {
                return Json(new LoginResult(0));
            }
            ScoutUp.Models.User user = db.Users.Where(e => e.UserEmail == email).Where(p => p.UserPassword == password).FirstOrDefault();
            if (user != null)
            {
                HttpContext.GetOwinContext().Authentication.SignOut();
                Session["id"] = user.UserID;
                   var ident = new ClaimsIdentity(
                 new[] { 
                 // adding following 2 claim just for supporting default antiforgery provider
                 new Claim(ClaimTypes.NameIdentifier, email),
                 new Claim("http://schemas.microsoft.com/accesscontrolservice/2010/07/claims/identityprovider", "ASP.NET Identity", "http://www.w3.org/2001/XMLSchema#string"),

                 new Claim(ClaimTypes.Name,email),
                  new Claim(ClaimTypes.PrimarySid,user.UserID.ToString()),

                 // optionally you could add roles if any
                 new Claim(ClaimTypes.Role, "User"),
                 },
                 DefaultAuthenticationTypes.ApplicationCookie);
                
                   HttpContext.GetOwinContext().Authentication.SignIn(
                      new AuthenticationProperties { IsPersistent = true }, ident);
                   return Json(new LoginResult(1));
            }
           else 
            {
                return Json(new LoginResult(0));
            }
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
        public ActionResult Create([Bind(Include = "UserName,UserSurname,UserPassword,UserEmail,UserCity,UserBirthDate,UserGender")] ScoutUp.Models. User user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Users.Add(user);
                    
                    db.SaveChanges();
                    return Json(new LoginResult(1));
                }
            }
            catch (DataException)
            {

                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }

            return Json(new LoginResult(0));
        }

        /// <summary>
        /// Profil update edildilten sonra ve ilk girişte çalışır id parametresi verimediği zaman zaten giriş yapmış olması gerektiği için sessiondan id parametresi alınarak devam edilir.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize]
        public ActionResult EditProfileBasic(int? id)
        {
            if (id == null)
            {
                var t = HttpContext.GetOwinContext().Authentication.User.Claims;
                foreach (var item in t)
                {
                    if (item.Type.Contains("primarysid"))
                    {
                        id = Convert.ToInt32(item.Value);
                        break;
                    }
                }
            }
            ScoutUp.Models.User user = db.Users.Find(id);
            if (HttpContext.GetOwinContext().Authentication.User.Identity.Name != user.UserEmail)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (user == null)
            {
                return HttpNotFound();
            }
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
        public ActionResult EditProfileBasic([Bind(Include = "UserID,UserName,UserSurname,UserEmail,UserCity,UserBirthDate,UserGender")]ScoutUp.Models.User user)
        {
            if (ModelState.IsValid)
            {
                db.Users.Attach(user);
                var entry = db.Entry(user);

                entry.Property(e => e.UserName).IsModified = true;
                entry.Property(e => e.UserSurname).IsModified = true;
                entry.Property(e => e.UserBirthDate).IsModified = true;
                entry.Property(e => e.UserCity).IsModified = true;
                entry.Property(e => e.UserGender).IsModified = true;
                db.SaveChanges();
                return View(user);
            }
            return Redirect("Home");
        }
        /// <summary>
        /// Sayfa yüklenirken kullanıcının seçmediği hobbileri oto komplete gönderir
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public ActionResult All()
        {
            var t = HttpContext.GetOwinContext().Authentication.User.Claims;
            int userID = 0;
            foreach (var item in t)
            {
                if (item.Type.Contains("primarysid"))
                    userID = Convert.ToInt32(item.Value);

            }
            User user = db.Users.Where(e => e.UserID == userID).FirstOrDefault();
            List<Hobbies> userHobbies = db.Hobbies.ToList();
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
            var t = HttpContext.GetOwinContext().Authentication.User.Claims;
            int userID = 0;
            foreach (var item in t)
            {
                if (item.Type.Contains("primarysid"))
                    userID = Convert.ToInt32(item.Value);

            }
            User user = db.Users.Where(e => e.UserID == userID).FirstOrDefault();
            return View(user);
        }
        /// <summary>
        /// Kullanıcı yeni hobi eklemek istediğinde burası çalışır 1 veya daha fazla hobi seçip gönderebilir. daha önce seçtiği hobiyi seçemediği için bu durum sorun olmuyor.
        /// </summary>
        /// <param name="HobbiesName"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        public ActionResult EditProfileInterests(string HobbiesName)
        {
            string[] split = HobbiesName.Split(',');
            List<Hobbies> hobbies = new List<Hobbies>();
            List<UserHobbies> userHobbies = new List<UserHobbies>();
            foreach (var item in split)
            {
                var ids = db.Hobbies.Where(e => e.HobbiesName == item).FirstOrDefault();
                hobbies.Add(ids);
            }
            var t = HttpContext.GetOwinContext().Authentication.User.Claims;
            int userID = 0;
            foreach (var item in t)
            {
                if (item.Type.Contains("primarysid"))
                    userID = Convert.ToInt32(item.Value);

            }
            foreach (var item in hobbies)
            {
                userHobbies.Add(new UserHobbies { UserID = userID, HobbiesID = item.HobbiesID });
            }
            db.UserHobbies.AddRange(userHobbies);
            db.SaveChanges();
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
            UserHobbies userHobbies = db.UserHobbies.Find(id);
            if (userHobbies == null)
            {
                return HttpNotFound();
            }
            db.UserHobbies.Remove(userHobbies);
            db.SaveChanges();
            return RedirectToAction("editprofileinterests", "Hobbies");
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
