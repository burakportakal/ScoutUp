using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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

            var userId = Convert.ToInt32(Context.User.Identity.GetUserId());
            return userId.ToString();
        }
    }
}