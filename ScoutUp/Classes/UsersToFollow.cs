using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ScoutUp.Classes
{
    public class UsersToFollow
    {

        public int id;
        public string Name;

        public UsersToFollow(int id, string name)
        {
            this.id = id;
            Name = name;
        }
    }
}