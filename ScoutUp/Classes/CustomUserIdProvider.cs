using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.Identity;

namespace ScoutUp.Classes
{
    public class CustomUserIdProvider :Hub, IUserIdProvider
    {
        public string GetUserId(IRequest request)
        {
            // your logic to fetch a user identifier goes here.

            // for example:

            var userId = Context.User.Identity.GetUserId();
            return userId.ToString();
        }
    }
}