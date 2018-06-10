using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ScoutUp.Models
{
    public class UsersLastMoves
    {
        public int UsersLastMovesID { get; set; }
        public string UserId { get; set; }
        public string UsersLastMoveText { get; set; }
        public string UsersMoveLink { get; set; }
        public DateTime MoveDate { get; set; }
        public virtual User User { get; set; }
    }
}