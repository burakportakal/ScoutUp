using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ScoutUp.Models
{
    public class Post
    {
        public int PostID { get; set; }
        public int UserID { get; set; }
        public string PostText { get; set; }
        public DateTime PostDatePosted { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<PostLikes> PostLikes { get; set; }
        public virtual ICollection<PostComments> PostComments { get; set; }
        public virtual ICollection<PostPhotos> PostPhotos { get; set; }
    }
}