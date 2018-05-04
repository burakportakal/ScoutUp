using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ScoutUp.Models
{
    public class PostPhotos
    {
        public int PostPhotosID { get; set; }
        public int PostID { get; set; }
        public string PostPhotosLocateSmall { get; set; }
        public string PostPhotosLocateBig { get; set; }
        public virtual Post Post { get; set; }
    }
}