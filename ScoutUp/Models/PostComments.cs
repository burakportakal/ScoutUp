using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ScoutUp.Models
{
    public class PostComments
    {
        public int PostCommentsID { get; set; }
        public int PostID { get; set; }
        public int UserID { get; set; }
        public string PostComment { get; set; }
        public DateTime PostCommentDate { get; set; }
        public virtual Post Post { get; set; }
       // public virtual User User { get; set; }

    }
}