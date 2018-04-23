using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ScoutUp.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            if (HttpContext.GetOwinContext().Authentication.User.Identity.IsAuthenticated)
                Response.Redirect("/home/newsfeed");
            return View();
        }
        [Authorize]
        public ActionResult Newsfeed()
        {
            string username = HttpContext.GetOwinContext().Authentication.User.Identity.Name;
            var type = HttpContext.GetOwinContext().Authentication.User.Identity.AuthenticationType;
            var t = HttpContext.GetOwinContext().Authentication.User.Claims;
            string id = "";
            foreach (var item in t)
            {
                if (item.Type.Contains("primarysid"))
                    id = item.Value;

            }
            var tt = HttpContext.GetOwinContext().Authentication.User.IsInRole("RoleName");
            ViewBag.email = username;
            ViewBag.id = id;
            return View();
        }
        public ActionResult Register()
        {
            return View();
        }
    
    }
}