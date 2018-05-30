using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ScoutUp.Models
{
    public class ChatMessages
    {
        public int ChatMessagesId { get; set; }
        public int ChatId { get; set; }
        public string ChatMessageText { get; set; }
        public DateTime ChatMessagesSendDate { get; set; }
        public virtual Chat Chat { get; set; }

    }
}