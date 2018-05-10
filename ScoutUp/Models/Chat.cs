using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ScoutUp.Models
{
    public class Chat
    {
        public int ChatId { get; set; }
        public int UserID { get; set; }
        public int OtherUserId { get; set; }
        public virtual ICollection<ChatMessages> ChatMessages {get; set; }
        public virtual User User { get; set; }
    }
}