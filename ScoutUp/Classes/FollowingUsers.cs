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
        public string ImagePath;

        public FollowingUsers(int id, string name,string imagePath)
        {
            this.id = id;
            Name = name;
            ImagePath = imagePath;
        }
        public FollowingUsers(int id, string name,bool userFollowing, string imagePath)
        {
            this.id = id;
            Name = name;
            ImagePath = imagePath;
            UserFollowing = userFollowing;
        }
    }
}