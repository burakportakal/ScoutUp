using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ScoutUp.Models
{
    public class UserRatings
    {
        public int UserRatingsID { get; set; }
        public int UserID { get; set; }
        public int CategoryItemID { get; set; }
        public double UserRating { get; set; }
        public virtual User User { get; set; }
        public virtual CategoryItems CategoryItems { get; set; }
    }
}