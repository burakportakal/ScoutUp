using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using ScoutUp.Models;
namespace ScoutUp.DAL
{
    public class ScoutUpInitializer: System.Data.Entity.DropCreateDatabaseAlways<ScoutUpDB>
    {
        protected override void Seed(ScoutUpDB context)
        {
            var users = new List<User> {
                new User {UserName="Burak",UserSurname="Portakal",UserPassword="1111",UserEmail="b@h.com",UserCity="Izmir",UserBirthDate=DateTime.Parse("1990-09-16") }
            };
            users.ForEach(s => context.Users.Add(s));
            context.SaveChanges();
            var Hobbies = new List<Hobbies> {
                new Hobbies { HobbiesName="Bilgisayar" },
                new Hobbies {HobbiesName="Spor" },
                new Hobbies { HobbiesName="Müzik" },
                new Hobbies {HobbiesName="Futbol" },
                new Hobbies { HobbiesName="Kodlama" },
                new Hobbies {HobbiesName="Java" },
                new Hobbies { HobbiesName="Asp" },
                new Hobbies {HobbiesName="Gitar" },
                new Hobbies { HobbiesName="Telefon" },
                new Hobbies {HobbiesName="Sosyal" }

            };
            Hobbies.ForEach(s => context.Hobbies.Add(s));
            context.SaveChanges();
            
            context.Roles.Add(new Microsoft.AspNet.Identity.EntityFramework.IdentityRole { Name = "user" });
        }
    }
}