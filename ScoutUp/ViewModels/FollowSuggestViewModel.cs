using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ScoutUp.ViewModels
{
    public class FollowSuggestViewModel
    {
        public string UserId { get; set; }
        public string Name { get; set; }
        public string ImagePath { get; set; }
        public double Similarity { get; set; }
        public string UserCity { get; set; }
    }
}