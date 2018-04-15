using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ScoutUp.Models
{
    public class UserPhotos
    {
        public int UserPhotosID { get; set; }
        public int UserID { get; set; }
        public string UserPhotoLocation { get; set; }
        public bool IsDeleted { get; set; }
        public virtual User User { get; set; }
    }
}