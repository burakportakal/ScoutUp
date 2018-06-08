using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ScoutUp.ViewModels
{
    public class MessageViewModel
    {
        public string UserId { get; set; }
        public string UserProfilePhoto { get; set; }
        public string UserName { get; set; }
        public string UserSurname { get; set; }
        public string RecieverUserId { get; set; }
        public string MessageText { get; set; }
        public DateTime DateSend { get; set; }
        public override bool Equals(object o)
        {
            return this.UserId == ((MessageViewModel)o).UserId;
        }

        public int GetHashCode(MessageViewModel obj)
        {
            throw new NotImplementedException();
        }
    }

    public class OnlineUsersViewModel
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string UserProfilePhoto { get; set; }
    }
}