using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ScoutUp.DAL;

namespace ScoutUp.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ScoutUpDB _db = new ScoutUpDB();
        // GET: Category
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Categories()
        {
            var ViewModel = _db.Categories.Select(e => new ViewModels.CategoryViewModel {CategoryId = e.CategoryID,CategoryName = e.CategoryName});
            return PartialView("Categories", ViewModel);
        }
    }
}