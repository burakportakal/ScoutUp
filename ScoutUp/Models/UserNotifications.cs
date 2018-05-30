using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ScoutUp.Models
{
    public class UserNotifications
    {
        public int UserNotificationsID { get; set; }
        public int UserID { get; set; }
        public string UserNotificationsMessage { get; set; }
        public DateTime UserNotificationsDate { get; set; }
        public bool UserNotificationsRead { get; set; }
        public string NotificationLink { get; set; }
        public virtual User User { get; set; }
    }
}