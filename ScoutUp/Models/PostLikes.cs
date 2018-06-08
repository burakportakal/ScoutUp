using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ScoutUp.Models
{
    public class PostLikes
    {
        public int PostLikesID { get; set; }
        public int PostID { get; set; }
        public string UserId { get; set; }
        public bool IsLiked { get; set; }
        public virtual Post Post { get; set; }
        //public virtual User User { get; set; }
    }
}