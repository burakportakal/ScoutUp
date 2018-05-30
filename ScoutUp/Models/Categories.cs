using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ScoutUp.Models
{
    public class Categories
    {
        [Key]
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public virtual ICollection<CategoryItems> CategoryItems { get; set; }
    }
}