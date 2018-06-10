using System;
using System.Collections.Generic;
using System.Linq;
using ScoutUp.DAL;
using ScoutUp.Models;
using ScoutUp.ViewModels;

namespace ScoutUp.Repository
{
    public class ChatMessageRepository
    {
        private readonly ScoutUp.DAL.ScoutUpDB _db= new ScoutUpDB();
        public void Add(MessageViewModel model)
        {
            StartMessaging(model.UserId,model.RecieverUserId);
            var chatId = _db.Chat
                .FirstOrDefault(e => (e.UserId == model.UserId && e.OtherUserId == model.RecieverUserId));
            var temp = new ChatMessages {ChatId = chatId.ChatId,ChatMessageText = model.MessageText,ChatMessagesSendDate = DateTime.Now};

            _db.ChatMessages.Add(temp);
            _db.SaveChanges();
        }

        public void StartMessaging(string userId,string otherUserId)
        {
            var alreadyInDb = _db.Chat.FirstOrDefault(e =>
                (e.UserId == userId && e.OtherUserId == otherUserId) )== null;
            if (alreadyInDb)
            {
                var chat = new Chat {UserId = userId, OtherUserId = otherUserId};
                _db.Chat.Add(chat);
                _db.SaveChanges();
            }
        }

        public List<MessageViewModel> GetAllMessagesBetweenUsers(string userId,string otherUserId)
        {
            var userAsSender = _db.Chat.FirstOrDefault(e =>
                                  (e.UserId == userId && e.OtherUserId == otherUserId));
            var userAsReciever = _db.Chat.FirstOrDefault(e =>
                (e.UserId == otherUserId && e.OtherUserId == userId));
            
           
            var userAsSenderchatMessages=new List<MessageViewModel>();
            var userAsRecieverChatMessages = new List<MessageViewModel>();
            if (userAsSender != null)
            {
                var user = userAsSender.User;
                userAsSenderchatMessages = _db.ChatMessages.Where(e => e.ChatId == userAsSender.ChatId).ToList()
                    .Select(t => new MessageViewModel
                    {
                        DateSend = t.ChatMessagesSendDate,
                        MessageText = t.ChatMessageText,
                        RecieverUserId = otherUserId,
                        UserId = userId,
                        UserName = user.UserFirstName,
                        UserSurname = user.UserSurname,
                        UserProfilePhoto = user.UserProfilePhoto
                    }).ToList();
            }

            if (userAsReciever != null)
            {
                var otherUser = userAsReciever.User;
                userAsRecieverChatMessages = _db.ChatMessages.Where(e => e.ChatId == userAsReciever.ChatId).ToList()
                    .Select(t => new MessageViewModel
                    {
                        DateSend = t.ChatMessagesSendDate,
                        MessageText = t.ChatMessageText,
                        RecieverUserId = userId,
                        UserId = otherUser.Id,
                        UserName = otherUser.UserFirstName,
                        UserSurname = otherUser.UserSurname,
                        UserProfilePhoto = otherUser.UserProfilePhoto
                    }).ToList();
            }

            List<MessageViewModel> combinedList = new List<MessageViewModel>();
            combinedList.AddRange(userAsSenderchatMessages);
            combinedList.AddRange(userAsRecieverChatMessages);
            combinedList=(combinedList.OrderByDescending(e => e.DateSend).Reverse()).ToList();
            return combinedList;
        }
    }
}