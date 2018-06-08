using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.SignalR;
using ScoutUp.Classes;
using ScoutUp.DAL;
using ScoutUp.Repository;
using ScoutUp.ViewModels;

namespace ScoutUp.Hubs
{
    public class MessageHub : Hub
    {
        public static readonly ConnectionMapping<string> _connections =
            NotificationHub._connections;

        private ScoutUp.DAL.ScoutUpDB _db = new ScoutUpDB();

        /// <summary>
        /// Kullancının gönderisi başka bir kullanıcı tarafından beğenildiğine bildirim gönderir.
        /// </summary>
        /// <param name="userid">Bildirim gönderilecek kullanıcının unique id si.</param>
        /// <param name="message">Bildirim gönderen kullanıcın adı soyadı varsa mesajı</param>
        /// <returns></returns>
        public Task SendMessage(string userid, string recieverUserId, string messageText)
        {
            var user = _db.Users.Find(userid);
            var message = new MessageViewModel
            {
                DateSend = DateTime.Now,
                UserId = userid,
                RecieverUserId = recieverUserId,
                MessageText = messageText,
                UserProfilePhoto = user.UserProfilePhoto,
                UserName = user.UserFirstName,
                UserSurname = user.UserSurname
            };
            ChatMessageRepository repository=new ChatMessageRepository();
            repository.Add(message);
            dynamic client = null;
            foreach (var connectionId in _connections.GetConnections(recieverUserId.ToString()))
            {
                client = Clients.Client(connectionId);
            }

            return client.recieveMessage(message);
        }

        public override System.Threading.Tasks.Task OnConnected()
        {
            //Create an instance of the Repository class
            string name = Context.User.Identity.GetUserId();
            var user = _db.Users.Find(name);
            var usersFollowing = user.UserFollow.Select(e => e.UserBeingFollowedUserId).ToArray();

            var followingEachOther = _db.UserFollow.Where(e => usersFollowing.Contains(e.UserId))
                .Where(e => e.UserBeingFollowedUserId == user.Id).Select(id => id.UserId).ToArray();
            List<OnlineUsersViewModel> model = new List<OnlineUsersViewModel>();
            List<string> connectionIds = new List<string>();
            foreach (var connection in followingEachOther)
            {
                var connectionid = _connections.GetConnections(connection.ToString());
                var enumerable = connectionid.ToList();
                if (enumerable.Any())
                {
                    connectionIds.Add(enumerable.First());
                    var onlineUser = _db.Users.Find(connection);
                    OnlineUsersViewModel temp = new OnlineUsersViewModel
                    {
                        UserId = onlineUser.Id,
                        UserName = onlineUser.UserFirstName + " " + onlineUser.UserSurname,
                        UserProfilePhoto = onlineUser.UserProfilePhoto
                    };
                    model.Add(temp);
                }
            }

            _connections.Add(name, Context.ConnectionId);
            dynamic client = null;
            foreach (var connectionId in _connections.GetConnections(name.ToString()))
            {
                client = Clients.Client(connectionId);
            }

            Clients.Clients(connectionIds).updateOnlineUsers(new OnlineUsersViewModel()
            {
                UserId = user.Id,
                UserName = user.UserFirstName + " " + user.UserSurname,
                UserProfilePhoto = user.UserProfilePhoto
            });
            client.onlineUsers(model);
            return base.OnConnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            string name = Context.User.Identity.GetUserId();
            _connections.Remove(name, Context.ConnectionId);
            Task.Run(() => WaiTask()).Wait();
            if (_connections.GetConnections(name).ToList().Count > 0)
            {
                return base.OnDisconnected(stopCalled);
            }
            var user = _db.Users.Find(name);
            var usersFollowing = user.UserFollow.Select(e => e.UserBeingFollowedUserId).ToArray();

            var followingEachOther = _db.UserFollow.Where(e => usersFollowing.Contains(e.UserId))
                .Where(e => e.UserBeingFollowedUserId == user.Id).Select(id => id.UserId).ToArray();
            List<string> connectionIds = new List<string>();
            foreach (var connection in followingEachOther)
            {
                var connectionid = _connections.GetConnections(connection.ToString());
                var enumerable = connectionid.ToList();
                if (enumerable.Any())
                {
                    connectionIds.Add(enumerable.First());
                }
            }

            Clients.Clients(connectionIds).updateUserOffline(new OnlineUsersViewModel()
            {
                UserId = user.Id,
                UserName = user.UserFirstName + " " + user.UserSurname,
                UserProfilePhoto = user.UserProfilePhoto
            });
            return base.OnDisconnected(stopCalled);
        }

        public async Task WaiTask()
        {
            await Task.Delay(10000);
        }
        public override Task OnReconnected()
        {
            string name = Context.User.Identity.GetUserId();

            if (!_connections.GetConnections(name).Contains(Context.ConnectionId))
            {
                _connections.Add(name, Context.ConnectionId);
            }

            return base.OnReconnected();
        }
    }
}