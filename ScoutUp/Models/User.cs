using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;

namespace ScoutUp.Models
{
    public enum UserGender
    {
        Erkek,
        Kadın
    }
    public class User : IdentityUser
    {
        public string UserFirstName { get; set; }
        public string UserSurname { get; set;}
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

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager, string authenticationType)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            // Add custom user claims here
            return userIdentity;
        }
    }
}