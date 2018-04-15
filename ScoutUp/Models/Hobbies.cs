using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ScoutUp.Models
{
    public class Hobbies
    {
        public int HobbiesID { get; set; }
        public string HobbiesName { get; set; }
        public virtual ICollection<UserHobbies> UserHobbies { get; set; }
    }
}