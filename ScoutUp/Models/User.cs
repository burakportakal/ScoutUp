using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ScoutUp.Models
{
    public class User
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string UserSurname { get; set;}
        public string UserPassword { get; set; }
        public string UserEmail { get; set; }
        public string UserCity { get; set; }
        public string UserAbout { get; set; }
        public DateTime UserBirthDate { get; set; }
        public bool UserGender { get; set; }
        public virtual ICollection<UserPhotos> UserPhotos { get; set; }
        public virtual ICollection<UserHobbies> UserHobbies { get; set; }
        public virtual ICollection<UserFollow> UserFollow { get; set; }
    }
}