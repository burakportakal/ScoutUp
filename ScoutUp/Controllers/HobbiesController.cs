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

namespace ScoutUp.Controllers
{
    public class HobbiesController : Controller
    {
        private ScoutUpDB db = new ScoutUpDB();
        // GET: Hobbies
        public ActionResult Index()
        {
            return View(db.Hobbies.ToList());
        }
        /// <summary>
        /// Sayfa yüklenirken kullanıcının seçmediği hobbileri oto komplete gönderir
        /// </summary>
        /// <returns></returns>
        public ActionResult All()
        {
            if (Session["email"] == null)
                Response.Redirect("/home");
            int userID = Convert.ToInt32(Session["id"].ToString());
            User user = db.Users.Where(e => e.UserID == userID).FirstOrDefault();
            List<Hobbies> userHobbies = db.Hobbies.ToList();
            foreach (var item in user.UserHobbies)
            {
                userHobbies.Remove(item.Hobbies);
            }
            return Json(userHobbies,JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// sayfa yüklenirken çalışan edit sayfası userı bulur gönderir burdan gelen veriyle seçtiği hobileri yeşil kutularda gösterir içerisinde userHobbiesid bulunduğu için silmesi kolay
        /// </summary>
        /// <returns></returns>
        public ActionResult EditProfileInterests()
        {
            if (Session["email"] == null)
                Response.Redirect("/home");
            int userID = Convert.ToInt32(Session["id"].ToString());
            User user = db.Users.Where(e => e.UserID == userID).FirstOrDefault();
            return View(user);
        }
        /// <summary>
        /// Kullanıcı yeni hobi eklemek istediğinde burası çalışır 1 veya daha fazla hobi seçip gönderebilir. daha önce seçtiği hobiyi seçemediği için bu durum sorun olmuyor.
        /// </summary>
        /// <param name="HobbiesName"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult EditProfileInterests(string HobbiesName)
        {
            if (Session["email"] == null)
                Response.Redirect("/home");
            string[] split = HobbiesName.Split(',');
            List<Hobbies> hobbies = new List<Hobbies>();
            List<UserHobbies> userHobbies = new List<UserHobbies>();
            foreach (var item in split)
            {
                var ids = db.Hobbies.Where(e => e.HobbiesName == item).FirstOrDefault();
                hobbies.Add(ids);
            }
            int userID =Convert.ToInt32( Session["id"].ToString());
            foreach (var item in hobbies)
            {
                userHobbies.Add(new UserHobbies { UserID = userID, HobbiesID = item.HobbiesID });
            }
            db.UserHobbies.AddRange(userHobbies);
            db.SaveChanges();
            return RedirectToAction("EditProfileInterests");
        }
        // GET: Hobbies/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hobbies hobbies = db.Hobbies.Find(id);
            if (hobbies == null)
            {
                return HttpNotFound();
            }
            return View(hobbies);
        }

        // GET: Hobbies/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Hobbies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "HobbiesID,HobbiesName")] Hobbies hobbies)
        {
            if (ModelState.IsValid)
            {
                db.Hobbies.Add(hobbies);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(hobbies);
        }

        // GET: Hobbies/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hobbies hobbies = db.Hobbies.Find(id);
            if (hobbies == null)
            {
                return HttpNotFound();
            }
            return View(hobbies);
        }

        // POST: Hobbies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "HobbiesID,HobbiesName")] Hobbies hobbies)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hobbies).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(hobbies);
        }

        // GET: Hobbies/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hobbies hobbies = db.Hobbies.Find(id);
            if (hobbies == null)
            {
                return HttpNotFound();
            }
            return View(hobbies);
        }

        // POST: Hobbies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Hobbies hobbies = db.Hobbies.Find(id);
            db.Hobbies.Remove(hobbies);
            db.SaveChanges();
            return RedirectToAction("Index");
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
