using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ScoutUp.Models
{
    public enum UserGender
    {
        Erkek,
        Kadın
    }
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
        public UserGender UserGender{ get; set; }
        public string UserProfilePhoto { get; set; }
        public bool IsFirstLogin { get; set; }
        public virtual ICollection<Post> UserPosts { get; set; }
        public virtual ICollection<UserPhotos> UserPhotos { get; set; }
        public virtual ICollection<UserHobbies> UserHobbies { get; set; }
        public virtual ICollection<UserFollow> UserFollow { get; set; }
        public virtual ICollection<UserNotifications> UserNotifications { get; set; }
        public virtual ICollection<UserRatings> UserRatings { get; set; }
        public virtual ICollection<Chat> Chat { get; set; }
        public virtual ICollection<UsersLastMoves> UsersLastMoves { get; set; }
    }
}