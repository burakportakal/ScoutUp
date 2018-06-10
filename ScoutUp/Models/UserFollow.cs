using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ScoutUp.Models
{
    public class UserFollow
    {
        public int UserFollowID { get; set; }
        public string UserId { get; set; }
        public string UserBeingFollowedUserId { get; set; }
        public bool IsFollowing { get; set; }
        public virtual User User { get; set; }
    }
}