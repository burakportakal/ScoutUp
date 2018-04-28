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
            Models.User user = getUserModel();
            ViewBag.email = user.UserEmail;
            ViewBag.id = user.UserID;
            UsersController controller = getUserController();
            ViewBag.followSuggest = controller.FollowSuggest();
            ViewBag.followerCount = controller.FollowerCount(user.UserID);
            return View();
        }
        public int usersFollowers()
        {
            UsersController controller = getUserController();
            Models.User user = getUserModel();
            int followerCount = controller.FollowerCount(user.UserID);
            return followerCount;
        }
        public ActionResult Register()
        {
            return View();
        }
        private Models.User getUserModel()
        {
            return getUserController().GetUser();
        }
        private UsersController getUserController()
        {
            var controller = DependencyResolver.Current.GetService<UsersController>();
            controller.ControllerContext = new ControllerContext(this.Request.RequestContext, controller);
            return controller;
        }
    
    }
}