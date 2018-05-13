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

        public ActionResult MessageTabId(int? id)
        {
            if (id != null)
            {
                var currentUserId = Convert.ToInt32(HttpContext.GetOwinContext().Authentication.User.Identity.GetUserId());
                var userMessagedWith = _db.Chat.Where(e => e.UserID == currentUserId).Select(e => e.OtherUserId).ToArray();
                var userMessagedWithAsReciever = _db.Chat.Where(e => e.OtherUserId == currentUserId).Select(e => e.UserID).ToArray();
                if (userMessagedWith.Contains((int) id) || userMessagedWithAsReciever.Contains((int) id))
                    return null;
                var user = _db.Users.Find(id);
                MessageViewModel model = new MessageViewModel
                {
                    UserId = user.UserID,
                    UserName = user.UserName,
                    UserSurname = user.UserSurname,
                    UserProfilePhoto = user.UserProfilePhoto,
                    DateSend = DateTime.Now
                };
                ViewBag.tabActive = true;
                ViewBag.targetId = id;
                return PartialView("MessageTab", new List<MessageViewModel> {model});
            }

            return null;
        }
        public ActionResult MessageTab(int? id)
        {
            var listModel = new List<MessageViewModel>();
            var currentUserId = Convert.ToInt32(HttpContext.GetOwinContext().Authentication.User.Identity.GetUserId());
            var userMessagedWith = _db.Chat.Where(e => e.UserID == currentUserId).Select(e => e.OtherUserId).ToArray();
            var userMessagedWithAsReciever = _db.Chat.Where(e => e.OtherUserId == currentUserId).Select(e => e.UserID).ToArray();
            var userMessagedListUsers = _db.Users.Where(e => userMessagedWith.Contains(e.UserID)).Select(e => new MessageViewModel
            {
                UserId = e.UserID,
                UserName = e.UserName,
                UserSurname = e.UserSurname,
                UserProfilePhoto = e.UserProfilePhoto,
            }).ToList();
            var userMessagedAsRecieverListUsers = _db.Users.Where(e => userMessagedWithAsReciever.Contains(e.UserID)).Select(e => new MessageViewModel
            {
                UserId = e.UserID,
                UserName = e.UserName,
                UserSurname = e.UserSurname,
                UserProfilePhoto = e.UserProfilePhoto,
            }).ToList();
            listModel.AddRange(userMessagedListUsers);
            foreach (var item in userMessagedAsRecieverListUsers)
            {
                
                if(!userMessagedListUsers.Contains(item))
                    listModel.Add(item);
            }

            if (id != null && (userMessagedWith.Contains((int) id) || userMessagedWithAsReciever.Contains((int) id)))
                ViewBag.targetId = id;
            else
                ViewBag.targetId = 0;
            ViewBag.tabActive = false;
            return PartialView("MessageTab", listModel);
        }

        public ActionResult MessageTabPaneId(int? id)
        {
            if (id != null)
            {
                var currentUserId =
                    Convert.ToInt32(HttpContext.GetOwinContext().Authentication.User.Identity.GetUserId());
                var userMessagedWith =
                    _db.Chat.Where(e => e.UserID == currentUserId).Select(e => e.OtherUserId).ToArray();
                var userMessagedWithAsReciever =
                    _db.Chat.Where(e => e.OtherUserId == currentUserId).Select(e => e.UserID).ToArray();
                if (userMessagedWith.Contains((int) id) || userMessagedWithAsReciever.Contains((int) id))
                    return null;
                var user = _db.Users.Find(id);
                var temp = new MessageViewModel
                {
                    UserId = user.UserID,
                    UserName = user.UserName,
                    UserSurname = user.UserSurname,
                    UserProfilePhoto = user.UserProfilePhoto,
                };
                var list = new List<MessageViewModel> {temp};
                ViewBag.tabPaneActive = true;
                ViewBag.targetId = id;
                return PartialView("MessageTabPane", list);
            }

            return null;
        }
        public ActionResult MessageTabPane(int? id)
        {

            var currentUserId =Convert.ToInt32( HttpContext.GetOwinContext().Authentication.User.Identity.GetUserId());
            var userMessagedWith = _db.Chat.Where(e => e.UserID == currentUserId).Select(e=> e.OtherUserId).ToArray();
            var userMessagedWithAsReciever = _db.Chat.Where(e => e.OtherUserId == currentUserId).Select(e => e.UserID).ToArray();
            var userMessagedListUsers = _db.Users.Where(e => userMessagedWith.Contains(e.UserID)).Select(e => new MessageViewModel
            {
                UserId = e.UserID,
                UserName = e.UserName,
                UserSurname = e.UserSurname,
                UserProfilePhoto = e.UserProfilePhoto,
            }).ToList();
            var userMessagedAsRecieverListUsers = _db.Users.Where(e => userMessagedWithAsReciever.Contains(e.UserID)).Select(e => new MessageViewModel
            {
                UserId = e.UserID,
                UserName = e.UserName,
                UserSurname = e.UserSurname,
                UserProfilePhoto = e.UserProfilePhoto,
            }).ToList();
         
            foreach (var item in userMessagedAsRecieverListUsers)
            {
                if (!userMessagedListUsers.Contains(item))
                    userMessagedListUsers.Add(item);
            }
            if (id != null && (userMessagedWith.Contains((int)id) || userMessagedWithAsReciever.Contains((int)id)))
                ViewBag.targetId = id;
            else
                ViewBag.targetId = 0;
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
            model.UserName = user.UserName;
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

        public ActionResult LoadMessagesBetweenUsers(int? targetUserId)
        {
            if (targetUserId == null) return null;
            var currentUserId =Convert.ToInt32( HttpContext.GetOwinContext().Authentication.User.Identity.GetUserId());
            var currentUser = _db.Users.Find(currentUserId);
            ChatMessageRepository repository=new ChatMessageRepository();
            ViewBag.currentUserId = currentUser.UserID;
            return PartialView("MessageLoader",repository.GetAllMessagesBetweenUsers(currentUser.UserID,(int) targetUserId));
        }
    }
}