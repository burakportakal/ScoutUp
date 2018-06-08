using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using ScoutUp.DAL;
using ScoutUp.Repository;
using ScoutUp.ViewModels;

namespace ScoutUp.Controllers
{
    [Authorize]
    public class MessageController : Controller
    {
        private readonly ScoutUp.DAL.ScoutUpDB _db = new ScoutUpDB();
        // GET: Message
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult MessageTabId(string id)
        {
            if (!String.IsNullOrEmpty(id))
            {
                var currentUserId = HttpContext.GetOwinContext().Authentication.User.Identity.GetUserId();
                var userMessagedWith =
                    _db.Chat.Where(e => e.UserId == currentUserId).Select(e => e.OtherUserId).ToArray();
                var userMessagedWithAsReciever =
                    _db.Chat.Where(e => e.OtherUserId == currentUserId).Select(e => e.UserId).ToArray();
                if (userMessagedWith.Contains(id) || userMessagedWithAsReciever.Contains(id))
                    return null;
                var user = _db.Users.Find(id);
                MessageViewModel model = new MessageViewModel
                {
                    UserId = user.Id,
                    UserName = user.UserFirstName,
                    UserSurname = user.UserSurname,
                    UserProfilePhoto = user.UserProfilePhoto,
                    DateSend = DateTime.Now
                };
                ViewBag.tabActive = true;
                ViewBag.targetId = id;
                return PartialView("MessageTab", new List<MessageViewModel> {model});
            }
            else
            {
                ViewBag.tabActive = false;
                ViewBag.targetId = "";
            }
            return null;
        }
        public ActionResult MessageTab(string id)
        {
            var listModel = new List<MessageViewModel>();
            var currentUserId = HttpContext.GetOwinContext().Authentication.User.Identity.GetUserId();
            var userMessagedWith = _db.Chat.Where(e => e.UserId == currentUserId).Select(e => e.OtherUserId).ToArray();
            var userMessagedWithAsReciever = _db.Chat.Where(e => e.OtherUserId == currentUserId).Select(e => e.UserId).ToArray();
            var userMessagedListUsers = _db.Users.Where(e => userMessagedWith.Contains(e.Id)).Select(e => new MessageViewModel
            {
                UserId = e.Id,
                UserName = e.UserFirstName,
                UserSurname = e.UserSurname,
                UserProfilePhoto = e.UserProfilePhoto,
            }).ToList();
            var userMessagedAsRecieverListUsers = _db.Users.Where(e => userMessagedWithAsReciever.Contains(e.Id)).Select(e => new MessageViewModel
            {
                UserId = e.Id,
                UserName = e.UserFirstName,
                UserSurname = e.UserSurname,
                UserProfilePhoto = e.UserProfilePhoto,
            }).ToList();
            listModel.AddRange(userMessagedListUsers);
            foreach (var item in userMessagedAsRecieverListUsers)
            {
                
                if(!userMessagedListUsers.Contains(item))
                    listModel.Add(item);
            }

            if (id != null && (userMessagedWith.Contains( id) || userMessagedWithAsReciever.Contains( id)))
                ViewBag.targetId = id;
            else
                ViewBag.targetId = "";
            ViewBag.tabActive = false;
            return PartialView("MessageTab", listModel);
        }

        public ActionResult MessageTabPaneId(string id)
        {
            if (!String.IsNullOrEmpty(id))
            {
                var currentUserId =
                    HttpContext.GetOwinContext().Authentication.User.Identity.GetUserId();
                var userMessagedWith =
                    _db.Chat.Where(e => e.UserId == currentUserId).Select(e => e.OtherUserId).ToArray();
                var userMessagedWithAsReciever =
                    _db.Chat.Where(e => e.OtherUserId == currentUserId).Select(e => e.UserId).ToArray();
                if (userMessagedWith.Contains( id) || userMessagedWithAsReciever.Contains( id))
                    return null;
                var user = _db.Users.Find(id);
                var temp = new MessageViewModel
                {
                    UserId = user.Id,
                    UserName = user.UserFirstName,
                    UserSurname = user.UserSurname,
                    UserProfilePhoto = user.UserProfilePhoto,
                };
                var list = new List<MessageViewModel> {temp};
                ViewBag.tabPaneActive = true;
                ViewBag.targetId = id;
                return PartialView("MessageTabPane", list);
            }
            else
            {
                ViewBag.tabPaneActive = false;
                ViewBag.targetId = "";
            }
            return null;
        }
        public ActionResult MessageTabPane(string id)
        {

            var currentUserId =HttpContext.GetOwinContext().Authentication.User.Identity.GetUserId();
            var userMessagedWith = _db.Chat.Where(e => e.UserId == currentUserId).Select(e=> e.OtherUserId).ToArray();
            var userMessagedWithAsReciever = _db.Chat.Where(e => e.OtherUserId == currentUserId).Select(e => e.UserId).ToArray();
            var userMessagedListUsers = _db.Users.Where(e => userMessagedWith.Contains(e.Id)).Select(e => new MessageViewModel
            {
                UserId = e.Id,
                UserName = e.UserFirstName,
                UserSurname = e.UserSurname,
                UserProfilePhoto = e.UserProfilePhoto,
            }).ToList();
            var userMessagedAsRecieverListUsers = _db.Users.Where(e => userMessagedWithAsReciever.Contains(e.Id)).Select(e => new MessageViewModel
            {
                UserId = e.Id,
                UserName = e.UserFirstName,
                UserSurname = e.UserSurname,
                UserProfilePhoto = e.UserProfilePhoto,
            }).ToList();
         
            foreach (var item in userMessagedAsRecieverListUsers)
            {
                if (!userMessagedListUsers.Contains(item))
                    userMessagedListUsers.Add(item);
            }
            if (id != null && (userMessagedWith.Contains(id) || userMessagedWithAsReciever.Contains(id)))
                ViewBag.targetId = id;
            else
                ViewBag.targetId = "";
            ViewBag.tabPaneActive = false;
            return PartialView("MessageTabPane", userMessagedListUsers);
        }

        public ActionResult MessageTabReciever(MessageViewModel model)
        {
            var listModel = new List<MessageViewModel>();
            listModel.Add(model);
            return PartialView("MessageTab", listModel);
        }

        public ActionResult MessageSender(MessageViewModel model)
        {
            var user = _db.Users.Find(model.UserId);
            model.UserProfilePhoto = user.UserProfilePhoto;
            model.DateSend=DateTime.Now;
            model.UserName = user.UserFirstName;
            model.UserSurname = user.UserSurname;
            return PartialView("MessageSender", model);
        }
        public ActionResult MessageReciever(MessageViewModel model)
        {
            return PartialView("MessageReciever", model);
        }

        public ActionResult OnlineUsers(OnlineUsersViewModel model )
        {
            var list = new List<OnlineUsersViewModel>();
            
            return PartialView("OnlineUsers", model);
        }

        public ActionResult LoadMessagesBetweenUsers(string targetUserId)
        {
            if (String.IsNullOrEmpty(targetUserId)) return null;
            var currentUserId = HttpContext.GetOwinContext().Authentication.User.Identity.GetUserId();
            var currentUser = _db.Users.Find(currentUserId);
            ChatMessageRepository repository=new ChatMessageRepository();
            ViewBag.currentUserId = currentUser.Id;
            return PartialView("MessageLoader",repository.GetAllMessagesBetweenUsers(currentUser.Id,targetUserId));
        }
    }
}