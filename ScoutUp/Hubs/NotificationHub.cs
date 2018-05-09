using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using ScoutUp.Classes;
using ScoutUp.Models;
using ScoutUp.Repository;

namespace ScoutUp.Hubs
{
    [HubName("notificationHub")]
    public class NotificationHub : Hub
    {
        public static readonly ConnectionMapping<string> _connections =
            new ConnectionMapping<string>();
        /// <summary>
        /// Kullancının gönderisi başka bir kullanıcı tarafından beğenildiğine bildirim gönderir.
        /// </summary>
        /// <param name="userid">Bildirim gönderilecek kullanıcının unique id si.</param>
        /// <param name="message">Bildirim gönderen kullanıcın adı soyadı varsa mesajı</param>
        /// <returns></returns>
        public Task SendNotification(int userid,string message,string notifyDirection,string notifyLink)
        {
            var objRepository = new NotificationRepository();
            var notification = objRepository.AddNotification(userid,message, notifyDirection, notifyLink);
            string name = Context.User.Identity.GetUserId();
            dynamic client = null;
            foreach (var connectionId in _connections.GetConnections(userid.ToString()))
            {
                client= Clients.Client(connectionId);
            }

            return client.updateNotification(notification);
        }
        public void UpdateNotification(List<UserNotifications> notifications)
        {
            var objRepository = new NotificationRepository();
            objRepository.UpdateNotification(notifications);
        }
        public override System.Threading.Tasks.Task OnConnected()
        {
            //Create an instance of the Repository class
            NotificationRepository objRepository = new NotificationRepository();
            string name = Context.User.Identity.GetUserId();

            _connections.Add(name, Context.ConnectionId);
            //refreshNotification is the client side method which will be writing in the future section. GetLogin() is a static extensions extract just the login name scrapping the domain name 
            //  Clients.User(Context.User.Identity.GetUserId()).refreshNotification(objRepository.GetUserNotifications(Convert.ToInt32(Context.User.Identity.GetUserId())));
            dynamic client = null;
            foreach (var connectionId in _connections.GetConnections(name.ToString()))
            {
                client = Clients.Client(connectionId);
            }

            List<UserNotifications> notifications = objRepository.GetUserNotifications(Convert.ToInt32(name));
            client.refreshNotification(notifications);
            return base.OnConnected();

        }
        public override Task OnDisconnected(bool stopCalled)
        {
            string name = Context.User.Identity.GetUserId();

            _connections.Remove(name, Context.ConnectionId);

            return base.OnDisconnected(stopCalled);
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