using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Hosting;

namespace ScoutUp.Classes
{
    public class UsersToFollow
    {

        public int id;
        public string Name;
        public string ImagePath;

        public UsersToFollow(int id, string name,string imagePath)
        {
            this.id = id;
            Name = name;
            ImagePath = imagePath;
        }
    }
}