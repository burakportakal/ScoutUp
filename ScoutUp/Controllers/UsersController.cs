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
            if (email == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ScoutUp.Models.User user = db.Users.Where(e => e.UserEmail == email).Where(p => p.UserPassword == password).FirstOrDefault();
            if (user == null)
            {
                return Json(new LoginResult(0));
            }
            Session["email"] = user.UserEmail;
            Session["id"] = user.UserID;
            return Json(new LoginResult(1));
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
        public ActionResult EditProfileBasic(int? id)
        {
            if (Session["email"] == null)
                Response.Redirect("/home");
            if (id == null)
            {
                id =Convert.ToInt32( Session["id"].ToString());
            }
            ScoutUp.Models.User user = db.Users.Find(id);
            if (Session["email"].ToString() != user.UserEmail)
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
