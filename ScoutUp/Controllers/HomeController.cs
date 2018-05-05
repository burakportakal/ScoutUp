using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ScoutUp.Classes;
using ScoutUp.DAL;
using ScoutUp.Models;
using ScoutUp.Search;

namespace ScoutUp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ScoutUpDB _db = new ScoutUpDB();
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
            var graph = new UserBfs();
            var usersToFollow = graph.Calculate(user.UserID);
            var followCount = user.UserFollow.Count;
            List<UsersToFollow> usersTofollowList = new List<UsersToFollow>();
            int counter = 0;
            foreach (var userid in usersToFollow)
            {
                if (counter != followCount+1)
                {
                    counter++;
                    continue;
                }
                var followUser = _db.Users.Find(userid);
                usersTofollowList.Add(new UsersToFollow (followUser.UserID,followUser.UserName + " " + followUser.UserSurname, followUser.UserProfilePhoto));
            }

            ViewBag.followSuggest = usersTofollowList;
            ViewBag.followerCount = controller.FollowerCount(user.UserID);
            return View(user);
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