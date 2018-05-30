using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ScoutUp.Models
{
    public class UserFollow
    {
        public int UserFollowID { get; set; }
        public int UserID { get; set; }
        public int UserBeingFollowedUserID { get; set; }
        public bool IsFollowing { get; set; }
        public virtual User User { get; set; }
    }
}