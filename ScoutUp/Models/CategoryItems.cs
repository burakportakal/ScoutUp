using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ScoutUp.Models
{
    public class CategoryItems
    {
        [Key]
        public int CategoryItemID { get; set; }
        public int CategoryID { get; set; }
        public string CategoryItemName { get; set; }
        public string CategoryItemPhoto { get; set; }
        public virtual Categories Categories { get; set; }
        public virtual ICollection<UserRatings> UserRatings { get; set; }
        
    }
}