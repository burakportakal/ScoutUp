﻿using System;
using System.Collections.Generic;
using ScoutUp.Models;
namespace ScoutUp.DAL
{
    public class ScoutUpInitializer: System.Data.Entity.DropCreateDatabaseIfModelChanges<ScoutUpDB>
    {
        protected override void Seed(ScoutUpDB context)
        {
            context.Database.ExecuteSqlCommand("DBCC CHECKIDENT ('User', RESEED, 1000000)");
            context.Database.ExecuteSqlCommand("DBCC CHECKIDENT ('Post', RESEED, 10000000)");
            context.Database.ExecuteSqlCommand("DBCC CHECKIDENT ('PostComments', RESEED, 100000000)");
            context.SaveChanges();
        var users = new List<User> {
                new User {UserName="Burak",UserSurname="Portakal",UserPassword="1111",UserEmail="b@h.com",UserCity="Izmir",UserAbout="Lorem ipsum",UserBirthDate=DateTime.Parse("1990-09-16")},
                new User {UserName="Kadir",UserSurname="Kanmaz",UserPassword="1111",UserEmail="kadir@h.com",UserCity="Denizli",UserAbout="Lorem ipsum",UserBirthDate=DateTime.Parse("1990-09-16") },
                new User {UserName="Onur",UserSurname="Sal",UserPassword="1111",UserEmail="onur@h.com",UserCity="Bursa",UserAbout="Lorem ipsum",UserBirthDate=DateTime.Parse("1990-09-16") },
                new User {UserName="Tayfun",UserSurname="Erturul",UserPassword="1111",UserEmail="tayfun@h.com",UserCity="Istanbul",UserAbout="Lorem ipsum",UserBirthDate=DateTime.Parse("1990-09-16") },
                new User {UserName="Mehmet",UserSurname="Akgül",UserPassword="1111",UserEmail="mehmet@h.com",UserCity="Konya",UserAbout="Lorem ipsum",UserBirthDate=DateTime.Parse("1990-09-16") },
                new User {UserName="Esra",UserSurname="Akgül",UserPassword="1111",UserEmail="esra@h.com",UserCity="Eskisehir",UserAbout="Lorem ipsum",UserBirthDate=DateTime.Parse("1990-09-16") },
                new User {UserName="Adem",UserSurname="Akgül",UserPassword="1111",UserEmail="adem@h.com",UserCity="Konya",UserAbout="Lorem ipsum",UserBirthDate=DateTime.Parse("1990-09-16") },
                new User {UserName="Sıtkı",UserSurname="Portakal",UserPassword="1111",UserEmail="sitki@h.com",UserCity="Izmir",UserAbout="Lorem ipsum",UserBirthDate=DateTime.Parse("1990-09-16") },
                new User {UserName="Hüseyin",UserSurname="Portakal",UserPassword="1111",UserEmail="huseyin@h.com",UserCity="Usak",UserAbout="Lorem ipsum",UserBirthDate=DateTime.Parse("1990-09-16") },
                new User {UserName="Lütfiye",UserSurname="Portakal",UserPassword="1111",UserEmail="lutfiye@h.com",UserCity="Izmir",UserAbout="Lorem ipsum",UserBirthDate=DateTime.Parse("1990-09-16") },
                new User {UserName="Emine",UserSurname="Amil",UserPassword="1111",UserEmail="emine@h.com",UserCity="Izmir",UserAbout="Lorem ipsum",UserBirthDate=DateTime.Parse("1990-09-16") },
                new User {UserName="Ali",UserSurname="Amil",UserPassword="1111",UserEmail="ali@h.com",UserCity="Izmir",UserAbout="Lorem ipsum",UserBirthDate=DateTime.Parse("1990-09-16") },
                new User {UserName="Mustafa",UserSurname="Amil",UserPassword="1111",UserEmail="mustafa@h.com",UserCity="Izmir",UserAbout="Lorem ipsum",UserBirthDate=DateTime.Parse("1990-09-16") },
                new User {UserName="Fatma",UserSurname="Amil",UserPassword="1111",UserEmail="fatma@h.com",UserCity="Izmir",UserAbout="Lorem ipsum",UserBirthDate=DateTime.Parse("1990-09-16") },

            };
            users.ForEach(s => context.Users.Add(s));
            context.SaveChanges();
            var hobbies = new List<Hobbies> {
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
            hobbies.ForEach(s => context.Hobbies.Add(s));
            context.SaveChanges();
            var userFollow = new List<UserFollow>
            {
                new UserFollow {UserID=1,UserBeingFollowedUserID=2,IsFollowing=true },
                new UserFollow {UserID=1,UserBeingFollowedUserID=3,IsFollowing=true },
                new UserFollow {UserID=1,UserBeingFollowedUserID=4,IsFollowing=true },
                new UserFollow {UserID=1,UserBeingFollowedUserID=5,IsFollowing=true },
                new UserFollow {UserID=1,UserBeingFollowedUserID=6,IsFollowing=true },
                new UserFollow {UserID=2,UserBeingFollowedUserID=1,IsFollowing=true },
                new UserFollow {UserID=2,UserBeingFollowedUserID=3,IsFollowing=true },
                new UserFollow {UserID=2,UserBeingFollowedUserID=6,IsFollowing=true },
                new UserFollow {UserID=2,UserBeingFollowedUserID=9,IsFollowing=true },
                new UserFollow {UserID=2,UserBeingFollowedUserID=8,IsFollowing=true },
                new UserFollow {UserID=3,UserBeingFollowedUserID=1,IsFollowing=true },
                new UserFollow {UserID=3,UserBeingFollowedUserID=2,IsFollowing=true },
                new UserFollow {UserID=3,UserBeingFollowedUserID=9,IsFollowing=true },
                new UserFollow {UserID=3,UserBeingFollowedUserID=6,IsFollowing=true },
                new UserFollow {UserID=3,UserBeingFollowedUserID=7,IsFollowing=true },
                new UserFollow {UserID=4,UserBeingFollowedUserID=1,IsFollowing=true },
                new UserFollow {UserID=4,UserBeingFollowedUserID=3,IsFollowing=true },
                new UserFollow {UserID=4,UserBeingFollowedUserID=10,IsFollowing=true },
                new UserFollow {UserID=4,UserBeingFollowedUserID=14,IsFollowing=true },
                new UserFollow {UserID=5,UserBeingFollowedUserID=10,IsFollowing=true },
                new UserFollow {UserID=5,UserBeingFollowedUserID=6,IsFollowing=true },
                new UserFollow {UserID=5,UserBeingFollowedUserID=7,IsFollowing=true },
                new UserFollow {UserID=5,UserBeingFollowedUserID=11,IsFollowing=true },
                new UserFollow {UserID=6,UserBeingFollowedUserID=10,IsFollowing=true },
                new UserFollow {UserID=6,UserBeingFollowedUserID=11,IsFollowing=true },
                new UserFollow {UserID=6,UserBeingFollowedUserID=12,IsFollowing=true },
                new UserFollow {UserID=6,UserBeingFollowedUserID=13,IsFollowing=true },
                new UserFollow {UserID=7,UserBeingFollowedUserID=5,IsFollowing=true },
                new UserFollow {UserID=7,UserBeingFollowedUserID=4,IsFollowing=true },
                new UserFollow {UserID=7,UserBeingFollowedUserID=2,IsFollowing=true },
                new UserFollow {UserID=7,UserBeingFollowedUserID=11,IsFollowing=true },
                new UserFollow {UserID=8,UserBeingFollowedUserID=12,IsFollowing=true },
                new UserFollow {UserID=8,UserBeingFollowedUserID=14,IsFollowing=true },
                new UserFollow {UserID=8,UserBeingFollowedUserID=9,IsFollowing=true },
                new UserFollow {UserID=8,UserBeingFollowedUserID=7,IsFollowing=true },
                new UserFollow {UserID=9,UserBeingFollowedUserID=1,IsFollowing=true },
                new UserFollow {UserID=9,UserBeingFollowedUserID=5,IsFollowing=true },
                new UserFollow {UserID=9,UserBeingFollowedUserID=10,IsFollowing=true },
                new UserFollow {UserID=9,UserBeingFollowedUserID=14,IsFollowing=true },
                new UserFollow {UserID=9,UserBeingFollowedUserID=2,IsFollowing=true },

            };
            userFollow.ForEach(s => context.UserFollow.Add(s));
            var posts = new List<Post>
            {
                new Post {PostID = 1,UserID = 7,PostDatePosted = DateTime.Now,PostText = "post 1 text deneme"},
                new Post {PostID = 2,UserID = 7,PostDatePosted = DateTime.Now,PostText = "post 2 text deneme"},
                new Post {PostID = 3,UserID = 7,PostDatePosted = DateTime.Now,PostText = "post 3 text deneme"}
            };
            posts.ForEach(s => context.Posts.Add(s));
            var comment1 =new List<PostComments>
            {
                new PostComments {PostComment = "deneme", PostID = 1, UserID = 1,PostCommentDate = DateTime.Now},
                new PostComments {PostComment = "deneme", PostID = 1, UserID = 2,PostCommentDate = DateTime.Now},
                new PostComments {PostComment = "deneme", PostID = 1, UserID = 3,PostCommentDate = DateTime.Now},
                new PostComments {PostComment = "deneme", PostID = 1, UserID = 1,PostCommentDate = DateTime.Now},
                new PostComments {PostComment = "deneme", PostID = 1, UserID = 3,PostCommentDate = DateTime.Now},
            };
            comment1.ForEach(s => context.PostComments.Add(s));
            var comment2 = new List<PostComments>
            {
                new PostComments {PostComment = "deneme", PostID = 2, UserID = 4,PostCommentDate = DateTime.Now},
                new PostComments {PostComment = "deneme", PostID = 2, UserID = 5,PostCommentDate = DateTime.Now},
            };
            comment2.ForEach(s => context.PostComments.Add(s));
            var comment3 = new List<PostComments>
            {
                new PostComments {PostComment = "deneme", PostID = 3, UserID = 3,PostCommentDate = DateTime.Now},
                new PostComments {PostComment = "deneme", PostID = 3, UserID = 2,PostCommentDate = DateTime.Now},
            };
            comment3.ForEach(s => context.PostComments.Add(s));
           
            context.SaveChanges();
            context.Roles.Add(new Microsoft.AspNet.Identity.EntityFramework.IdentityRole { Name = "user" });
        }
    }
}