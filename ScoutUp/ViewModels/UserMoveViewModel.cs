using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ScoutUp.ViewModels
{
    public class UserMoveViewModel
    {
        public string UsersLastMoveText { get; set; }
        public string UsersMoveLink { get; set; }
        public DateTime MoveDate { get; set; }
    }
}