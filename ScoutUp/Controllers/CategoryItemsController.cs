using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ScoutUp.DAL;
using ScoutUp.Models;

namespace ScoutUp.Controllers
{
    public class CategoryItemsController : Controller
    {
        private ScoutUpDB db = new ScoutUpDB();

        // GET: CategoryItems
        public async Task<ActionResult> Index()
        {
            var categoryItems = db.CategoryItems.Include(c => c.Categories);
            return View(await categoryItems.ToListAsync());
        }

        // GET: CategoryItems/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CategoryItems categoryItems = await db.CategoryItems.FindAsync(id);
            if (categoryItems == null)
            {
                return HttpNotFound();
            }
            return View(categoryItems);
        }

        // GET: CategoryItems/Create
        public ActionResult Create()
        {
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryName");
            return View();
        }

        // POST: CategoryItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "CategoryItemID,CategoryID,CategoryItemName,CategoryItemPhoto")] CategoryItems categoryItems)
        {
            if (ModelState.IsValid)
            {
                db.CategoryItems.Add(categoryItems);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryName", categoryItems.CategoryID);
            return View(categoryItems);
        }

        // GET: CategoryItems/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CategoryItems categoryItems = await db.CategoryItems.FindAsync(id);
            if (categoryItems == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryName", categoryItems.CategoryID);
            return View(categoryItems);
        }

        // POST: CategoryItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "CategoryItemID,CategoryID,CategoryItemName,CategoryItemPhoto")] CategoryItems categoryItems)
        {
            if (ModelState.IsValid)
            {
                db.Entry(categoryItems).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryName", categoryItems.CategoryID);
            return View(categoryItems);
        }

        // GET: CategoryItems/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CategoryItems categoryItems = await db.CategoryItems.FindAsync(id);
            if (categoryItems == null)
            {
                return HttpNotFound();
            }
            return View(categoryItems);
        }

        // POST: CategoryItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            CategoryItems categoryItems = await db.CategoryItems.FindAsync(id);
            db.CategoryItems.Remove(categoryItems);
            await db.SaveChangesAsync();
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
