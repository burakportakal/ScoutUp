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
    public class UserHobbiesController : Controller
    {
        private ScoutUpDB db = new ScoutUpDB();

        // GET: UserHobbies
        public ActionResult Index()
        {
            var userHobbies = db.UserHobbies.Include(u => u.Hobbies).Include(u => u.User);
            return View(userHobbies.ToList());
        }

        // GET: UserHobbies/Details/5
        public ActionResult Details(int? id)
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
            return View(userHobbies);
        }

        // GET: UserHobbies/Create
        public ActionResult Create()
        {
            ViewBag.HobbiesID = new SelectList(db.Hobbies, "HobbiesID", "HobbiesName");
            ViewBag.UserID = new SelectList(db.Users, "UserID", "UserName");
            return View();
        }

        // POST: UserHobbies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserHobbiesID,UserID,HobbiesID")] UserHobbies userHobbies)
        {
            if (ModelState.IsValid)
            {
                db.UserHobbies.Add(userHobbies);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.HobbiesID = new SelectList(db.Hobbies, "HobbiesID", "HobbiesName", userHobbies.HobbiesID);
            ViewBag.UserID = new SelectList(db.Users, "UserID", "UserName", userHobbies.UserID);
            return View(userHobbies);
        }
        /// <summary>
        /// tıklanan hobiyi siler
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: UserHobbies/Delete/5
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
            return RedirectToAction("editprofileinterests","Hobbies");
        }

        // POST: UserHobbies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UserHobbies userHobbies = db.UserHobbies.Find(id);
            db.UserHobbies.Remove(userHobbies);
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
