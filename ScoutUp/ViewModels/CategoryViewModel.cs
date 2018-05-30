using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ScoutUp.ViewModels
{
    public class CategoryViewModel
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
    }

    public class CategoryItemViewModel
    {
        public int CategoryItemId { get; set; }
        public string CategoryItemName { get; set; }
        public int CategoryId { get; set; }
        public string CategoryItemPhoto { get; set; }
        public double UserRating { get; set; }
    }

    public class RatingViewModel
    {
        public int ItemId { get; set; }
        public int Rating { get; set; }
    }
}