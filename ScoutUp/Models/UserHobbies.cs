using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ScoutUp.Models
{
    public class UserHobbies
    {
        public int UserHobbiesID { get; set; }
        public int UserID { get; set; }
        public int HobbiesID { get; set; }
        public virtual User User { get; set; }
        public virtual Hobbies Hobbies { get; set; }
    }
}