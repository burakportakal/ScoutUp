using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ScoutUp.Classes
{
    public class FollowingUsers
    {
        public int id;
        public string Name;
        public bool UserFollowing;

        public FollowingUsers(int id, string name)
        {
            this.id = id;
            Name = name;
        }
        public FollowingUsers(int id, string name,bool userFollowing)
        {
            this.id = id;
            Name = name;
            UserFollowing = userFollowing;
        }
    }
}