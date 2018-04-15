using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using ScoutUp.Models;
namespace ScoutUp.DAL
{
    public class ScoutUpInitializer: System.Data.Entity.DropCreateDatabaseIfModelChanges<ScoutUpDB>
    {
        protected override void Seed(ScoutUpDB context)
        {
            var users = new List<User> {
                new User {UserName="Burak",UserSurname="Portakal",UserPassword="1111",UserEmail="b@h.com",UserCity="Izmir",UserBirthDate=DateTime.Parse("1990-09-16") }
            };
            users.ForEach(s => context.Users.Add(s));
            context.SaveChanges();

        }
    }
}