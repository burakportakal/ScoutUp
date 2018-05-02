using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ScoutUp.Models
{
    public class PostPhotosLocation
    {
        public int PostPhotosLocationID { get; set; }
        public int PostPhotosID { get; set; }
        public string PostPhotosLocateSmall { get; set; }
        public string PostPhotosLocateBig { get; set; }
        public virtual PostPhotos PhosPhotos { get; set; }
    }
}