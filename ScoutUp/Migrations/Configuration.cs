using System.Collections.Generic;
using ScoutUp.Models;

namespace ScoutUp.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ScoutUp.DAL.ScoutUpDB>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(ScoutUp.DAL.ScoutUpDB context)
        {
            Dictionary<int,string> userIds= new Dictionary<int, string>();
            var users = new List<User>
            {
                new User
                {
                    UserName = "Burak",
                    UserFirstName = "Burak",
                    UserSurname = "Portakal",
                    PasswordHash = "AHjgVHzFJ+t9cxaSSKnvaaEW/TM/FNZtNuHSIhuJkpW2RGkA8kWRMFUP0Fv3z8pqyQ==",
                    Email = "b@h.com",
                    UserCity = "Izmir",
                    UserAbout = "Lorem ipsum",
                    UserBirthDate = DateTime.Parse("1990-09-16"),
                    UserProfilePhoto = "../../images/post-images/user-1-tumbnail.jpg",
                    TwoFactorEnabled = false,
                    EmailConfirmed = false,
                    LockoutEnabled = false
                },
                new User
                {
                    UserName = "Kadir",
                    UserFirstName = "Kadir",
                    UserSurname = "Kanmaz",
                    PasswordHash = "AHjgVHzFJ+t9cxaSSKnvaaEW/TM/FNZtNuHSIhuJkpW2RGkA8kWRMFUP0Fv3z8pqyQ==",
                    Email = "kadir@h.com",
                    UserCity = "Denizli",
                    UserAbout = "Lorem ipsum",
                    UserBirthDate = DateTime.Parse("1990-09-16"),
                    UserProfilePhoto = "../../images/post-images/user-2-tumbnail.jpg"
                },
                new User
                {
                    UserName = "Onur",
                    UserFirstName = "Onur",
                    UserSurname = "Sal",
                    PasswordHash = "AHjgVHzFJ+t9cxaSSKnvaaEW/TM/FNZtNuHSIhuJkpW2RGkA8kWRMFUP0Fv3z8pqyQ==",
                    Email = "onur@h.com",
                    UserCity = "Bursa",
                    UserAbout = "Lorem ipsum",
                    UserBirthDate = DateTime.Parse("1990-09-16"),
                    UserProfilePhoto = "../../images/post-images/user-3-tumbnail.jpg"
                },
                new User
                {
                    UserName = "Tayfun",
                    UserFirstName = "Tayfun",
                    UserSurname = "Erturul",
                    PasswordHash = "AHjgVHzFJ+t9cxaSSKnvaaEW/TM/FNZtNuHSIhuJkpW2RGkA8kWRMFUP0Fv3z8pqyQ==",
                    Email = "tayfun@h.com",
                    UserCity = "Istanbul",
                    UserAbout = "Lorem ipsum",
                    UserBirthDate = DateTime.Parse("1990-09-16"),
                    UserProfilePhoto = "../../images/post-images/user-4-tumbnail.jpg"
                },
                new User
                {
                    UserName = "Mehmet",
                    UserFirstName = "Mehmet",
                    UserSurname = "Akgül",
                    PasswordHash = "AHjgVHzFJ+t9cxaSSKnvaaEW/TM/FNZtNuHSIhuJkpW2RGkA8kWRMFUP0Fv3z8pqyQ==",
                    Email = "mehmet@h.com",
                    UserCity = "Konya",
                    UserAbout = "Lorem ipsum",
                    UserBirthDate = DateTime.Parse("1990-09-16"),
                    UserProfilePhoto = "../../images/post-images/user-5-tumbnail.jpg"
                },
                new User
                {
                    UserName = "Esra",
                    UserFirstName = "Esra",
                    UserSurname = "Akgül",
                    PasswordHash = "AHjgVHzFJ+t9cxaSSKnvaaEW/TM/FNZtNuHSIhuJkpW2RGkA8kWRMFUP0Fv3z8pqyQ==",
                    Email = "esra@h.com",
                    UserCity = "Eskisehir",
                    UserAbout = "Lorem ipsum",
                    UserBirthDate = DateTime.Parse("1990-09-16"),
                    UserProfilePhoto = "../../images/post-images/user-6-tumbnail.jpg"
                },
                new User
                {
                    UserName = "Adem",
                    UserFirstName = "Adem",
                    UserSurname = "Akgül",
                    PasswordHash = "AHjgVHzFJ+t9cxaSSKnvaaEW/TM/FNZtNuHSIhuJkpW2RGkA8kWRMFUP0Fv3z8pqyQ==",
                    Email = "adem@h.com",
                    UserCity = "Konya",
                    UserAbout = "Lorem ipsum",
                    UserBirthDate = DateTime.Parse("1990-09-16"),
                    UserProfilePhoto = "../../images/post-images/user-7-tumbnail.jpg"
                },
                new User
                {
                    UserName = "Sýtký",
                    UserFirstName = "Sýtký",
                    UserSurname = "Portakal",
                    PasswordHash = "AHjgVHzFJ+t9cxaSSKnvaaEW/TM/FNZtNuHSIhuJkpW2RGkA8kWRMFUP0Fv3z8pqyQ==",
                    Email = "sitki@h.com",
                    UserCity = "Izmir",
                    UserAbout = "Lorem ipsum",
                    UserBirthDate = DateTime.Parse("1990-09-16"),
                    UserProfilePhoto = "../../images/post-images/user-8-tumbnail.jpg"
                },
                new User
                {
                    UserName = "Hüseyin",
                    UserFirstName = "Hüseyin",
                    UserSurname = "Portakal",
                    PasswordHash = "AHjgVHzFJ+t9cxaSSKnvaaEW/TM/FNZtNuHSIhuJkpW2RGkA8kWRMFUP0Fv3z8pqyQ==",
                    Email = "huseyin@h.com",
                    UserCity = "Usak",
                    UserAbout = "Lorem ipsum",
                    UserBirthDate = DateTime.Parse("1990-09-16"),
                    UserProfilePhoto = "../../images/post-images/user-9-tumbnail.jpg"
                },
                new User
                {
                    UserName = "Lütfiye",
                    UserFirstName = "Lütfiye",
                    UserSurname = "Portakal",
                    PasswordHash = "AHjgVHzFJ+t9cxaSSKnvaaEW/TM/FNZtNuHSIhuJkpW2RGkA8kWRMFUP0Fv3z8pqyQ==",
                    Email = "lutfiye@h.com",
                    UserCity = "Izmir",
                    UserAbout = "Lorem ipsum",
                    UserBirthDate = DateTime.Parse("1990-09-16"),
                    UserProfilePhoto = "../../images/post-images/user-10-tumbnail.jpg"
                },
                new User
                {
                    UserName = "Emine",
                    UserFirstName = "Emine",
                    UserSurname = "Amil",
                    PasswordHash = "AHjgVHzFJ+t9cxaSSKnvaaEW/TM/FNZtNuHSIhuJkpW2RGkA8kWRMFUP0Fv3z8pqyQ==",
                    Email = "emine@h.com",
                    UserCity = "Izmir",
                    UserAbout = "Lorem ipsum",
                    UserBirthDate = DateTime.Parse("1990-09-16"),
                    UserProfilePhoto = "../../images/post-images/user-11-tumbnail.jpg"
                },
                new User
                {
                    UserName = "Ali",
                    UserFirstName = "Ali",
                    UserSurname = "Amil",
                    PasswordHash = "AHjgVHzFJ+t9cxaSSKnvaaEW/TM/FNZtNuHSIhuJkpW2RGkA8kWRMFUP0Fv3z8pqyQ==",
                    Email = "ali@h.com",
                    UserCity = "Izmir",
                    UserAbout = "Lorem ipsum",
                    UserBirthDate = DateTime.Parse("1990-09-16"),
                    UserProfilePhoto = "../../images/post-images/user-12-tumbnail.jpg"
                },
                new User
                {
                    UserName = "Mustafa",
                    UserFirstName = "Mustafa",
                    UserSurname = "Amil",
                    PasswordHash = "AHjgVHzFJ+t9cxaSSKnvaaEW/TM/FNZtNuHSIhuJkpW2RGkA8kWRMFUP0Fv3z8pqyQ==",
                    Email = "mustafa@h.com",
                    UserCity = "Izmir",
                    UserAbout = "Lorem ipsum",
                    UserBirthDate = DateTime.Parse("1990-09-16"),
                    UserProfilePhoto = "../../images/post-images/user-13-tumbnail.jpg"
                },
                new User
                {
                    UserName = "Fatma",
                    UserFirstName = "Fatma",
                    UserSurname = "Amil",
                    PasswordHash = "AHjgVHzFJ+t9cxaSSKnvaaEW/TM/FNZtNuHSIhuJkpW2RGkA8kWRMFUP0Fv3z8pqyQ==",
                    Email = "fatma@h.com",
                    UserCity = "Izmir",
                    UserAbout = "Lorem ipsum",
                    UserBirthDate = DateTime.Parse("1990-09-16"),
                    UserProfilePhoto = "../../images/post-images/user-14-tumbnail.jpg"
                },

            };
            int counter = 0;
            foreach (var user in users)
            {
                counter++;
                context.Users.Add(user);
                context.SaveChanges();
                userIds.Add(counter, user.Id);
                
            }
           
            var userPhotos = new List<UserPhotos>
            {
                new UserPhotos
                {
                    UserId = userIds[1],
                    UserPhotoBig = "user-1-big.jpg",
                    UserPhotoSmall = "../../images/post-images/user-1-tumbnail.jpg",
                },
                new UserPhotos
                {
                    UserId = userIds[2],
                    UserPhotoBig = "user-2-big.jpg",
                    UserPhotoSmall = "../../images/post-images/user-2-tumbnail.jpg",
                },
                new UserPhotos
                {
                    UserId = userIds[3],
                    UserPhotoBig = "user-3-big.jpg",
                    UserPhotoSmall = "../../images/post-images/user-3-tumbnail.jpg",
                },
                new UserPhotos
                {
                    UserId = userIds[4],
                    UserPhotoBig = "user-4-big.jpg",
                    UserPhotoSmall = "../../images/post-images/user-4-tumbnail.jpg",
                },
                new UserPhotos
                {
                    UserId = userIds[5],
                    UserPhotoBig = "user-5-big.jpg",
                    UserPhotoSmall = "../../images/post-images/user-5-tumbnail.jpg",
                },
                new UserPhotos
                {
                    UserId = userIds[6],
                    UserPhotoBig = "user-6-big.jpg",
                    UserPhotoSmall = "../../images/post-images/user-6-tumbnail.jpg",
                },
                new UserPhotos
                {
                    UserId = userIds[7],
                    UserPhotoBig = "user-7-big.jpg",
                    UserPhotoSmall = "../../images/post-images/user-7-tumbnail.jpg",
                },
                new UserPhotos
                {
                    UserId = userIds[8],
                    UserPhotoBig = "user-8-big.jpg",
                    UserPhotoSmall = "../../images/post-images/user-8-tumbnail.jpg",
                },
                new UserPhotos
                {
                    UserId = userIds[9],
                    UserPhotoBig = "user-9-big.jpg",
                    UserPhotoSmall = "../../images/post-images/user-9-tumbnail.jpg",
                },
                new UserPhotos
                {
                    UserId = userIds[10],
                    UserPhotoBig = "user-10-big.jpg",
                    UserPhotoSmall = "../../images/post-images/user-10-tumbnail.jpg",
                },
                new UserPhotos
                {
                    UserId = userIds[11],
                    UserPhotoBig = "user-11-big.jpg",
                    UserPhotoSmall = "../../images/post-images/user-11-tumbnail.jpg",
                },
                new UserPhotos
                {
                    UserId = userIds[12],
                    UserPhotoBig = "user-12-big.jpg",
                    UserPhotoSmall = "../../images/post-images/user-12-tumbnail.jpg",
                },
                new UserPhotos
                {
                    UserId = userIds[13],
                    UserPhotoBig = "user-13-big.jpg",
                    UserPhotoSmall = "../../images/post-images/user-13-tumbnail.jpg",
                },
                new UserPhotos
                {
                    UserId = userIds[14],
                    UserPhotoBig = "user-14-big.jpg",
                    UserPhotoSmall = "../../images/post-images/user-14-tumbnail.jpg",
                },
            };
            
            var hobbies = new List<Hobbies>
            {
                new Hobbies {HobbiesName = "Bilgisayar"}, //1
                new Hobbies {HobbiesName = "Spor"}, //2
                new Hobbies {HobbiesName = "Müzik"}, //3
                new Hobbies {HobbiesName = "Futbol"}, //4
                new Hobbies {HobbiesName = "Kodlama"}, //5
                new Hobbies {HobbiesName = "Java"}, //6
                new Hobbies {HobbiesName = "Asp"}, //7
                new Hobbies {HobbiesName = "Gitar"}, //8
                new Hobbies {HobbiesName = "Telefon"}, //9
                new Hobbies {HobbiesName = "Sosyal"} //10

            };
            hobbies.ForEach(s => context.Hobbies.Add(s));
            context.SaveChanges();
            var userFollow = new List<UserFollow>
            {
                new UserFollow {UserId = userIds[1],  UserBeingFollowedUserId = userIds[2], IsFollowing = true},
                new UserFollow {UserId = userIds[1],  UserBeingFollowedUserId = userIds[3], IsFollowing = true},
                new UserFollow {UserId = userIds[1],  UserBeingFollowedUserId = userIds[4], IsFollowing = true},
                new UserFollow {UserId = userIds[1],  UserBeingFollowedUserId = userIds[5], IsFollowing = true},
                new UserFollow {UserId = userIds[2],  UserBeingFollowedUserId = userIds[6], IsFollowing = true},
                new UserFollow {UserId = userIds[2],  UserBeingFollowedUserId = userIds[1], IsFollowing = true},
                new UserFollow {UserId = userIds[2],  UserBeingFollowedUserId = userIds[3], IsFollowing = true},
                new UserFollow {UserId = userIds[3],  UserBeingFollowedUserId = userIds[6], IsFollowing = true},
                new UserFollow {UserId = userIds[3],  UserBeingFollowedUserId = userIds[9], IsFollowing = true},
                new UserFollow {UserId = userIds[3],  UserBeingFollowedUserId = userIds[8], IsFollowing = true},
                new UserFollow {UserId = userIds[4],  UserBeingFollowedUserId = userIds[1], IsFollowing = true},
                new UserFollow {UserId = userIds[4],  UserBeingFollowedUserId = userIds[2], IsFollowing = true},
                new UserFollow {UserId = userIds[4],  UserBeingFollowedUserId = userIds[9], IsFollowing = true},
                new UserFollow {UserId = userIds[5],  UserBeingFollowedUserId = userIds[6], IsFollowing = true},
                new UserFollow {UserId = userIds[5],  UserBeingFollowedUserId = userIds[7], IsFollowing = true},
                new UserFollow {UserId = userIds[5],  UserBeingFollowedUserId = userIds[1], IsFollowing = true},
                new UserFollow {UserId = userIds[6],  UserBeingFollowedUserId = userIds[3], IsFollowing = true},
                new UserFollow {UserId = userIds[6],  UserBeingFollowedUserId = userIds[10], IsFollowing = true},
                new UserFollow {UserId = userIds[6],  UserBeingFollowedUserId = userIds[14], IsFollowing = true},
                new UserFollow {UserId = userIds[7],  UserBeingFollowedUserId = userIds[10], IsFollowing = true},
                new UserFollow {UserId = userIds[7],  UserBeingFollowedUserId = userIds[6], IsFollowing = true},
                new UserFollow {UserId = userIds[7],  UserBeingFollowedUserId = userIds[8], IsFollowing = true},
                new UserFollow {UserId = userIds[8],  UserBeingFollowedUserId = userIds[11], IsFollowing = true},
                new UserFollow {UserId = userIds[8],  UserBeingFollowedUserId = userIds[10], IsFollowing = true},
                new UserFollow {UserId = userIds[8],  UserBeingFollowedUserId = userIds[12], IsFollowing = true},
                new UserFollow {UserId = userIds[9],  UserBeingFollowedUserId = userIds[12], IsFollowing = true},
                new UserFollow {UserId = userIds[9],  UserBeingFollowedUserId = userIds[13], IsFollowing = true},
                new UserFollow {UserId = userIds[9],  UserBeingFollowedUserId = userIds[5], IsFollowing = true},
                new UserFollow {UserId = userIds[10], UserBeingFollowedUserId = userIds[4], IsFollowing = true},
                new UserFollow {UserId = userIds[10], UserBeingFollowedUserId = userIds[2], IsFollowing = true},
                new UserFollow {UserId = userIds[10], UserBeingFollowedUserId = userIds[11], IsFollowing = true},
                new UserFollow {UserId = userIds[11], UserBeingFollowedUserId = userIds[12], IsFollowing = true},
                new UserFollow {UserId = userIds[11], UserBeingFollowedUserId = userIds[14], IsFollowing = true},
                new UserFollow {UserId = userIds[11], UserBeingFollowedUserId = userIds[1], IsFollowing = true},
                new UserFollow {UserId = userIds[12], UserBeingFollowedUserId = userIds[7], IsFollowing = true},
                new UserFollow {UserId = userIds[12], UserBeingFollowedUserId = userIds[1], IsFollowing = true},
                new UserFollow {UserId = userIds[12], UserBeingFollowedUserId = userIds[5], IsFollowing = true},
                new UserFollow {UserId = userIds[13], UserBeingFollowedUserId = userIds[10], IsFollowing = true},
                new UserFollow {UserId = userIds[13], UserBeingFollowedUserId = userIds[14], IsFollowing = true},
                new UserFollow {UserId = userIds[14], UserBeingFollowedUserId = userIds[11], IsFollowing = true},

            };
            userFollow.ForEach(s => context.UserFollow.Add(s));
            var posts = new List<Post>
            {
                new Post
                {
                    PostID = 1,
                    UserId = userIds[7],
                    PostDatePosted = DateTime.Now,
                    PostText =
                        "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nulla tincidunt pharetra velit ut ullamcorper. Curabitur eget enim vitae purus convallis laoreet. Nulla facilisi. Sed rhoncus egestas nisl vitae hendrerit. Sed justo felis, vestibulum ac dignissim sed, rhoncus vel lorem. Sed viverra enim sed pulvinar egestas. Fusce sapien felis, ullamcorper vitae nunc sit amet, vulputate faucibus ex. Vivamus commodo dictum purus, non scelerisque mauris faucibus eu."
                },
                new Post
                {
                    PostID = 2,
                    UserId = userIds[7],
                    PostDatePosted = DateTime.Now,
                    PostText =
                        "Aliquam tempor ligula at diam feugiat, eu dignissim enim ultrices. Proin nec fringilla eros. Vivamus sollicitudin elit et risus laoreet aliquam. Integer scelerisque feugiat purus ac aliquet. Praesent sed mi lobortis, mattis metus at, sagittis eros. Quisque tempus porttitor iaculis. Morbi eget nunc felis."
                },
                new Post
                {
                    PostID = 3,
                    UserId = userIds[7],
                    PostDatePosted = DateTime.Now,
                    PostText =
                        "Vestibulum semper, enim id tincidunt tempor, metus tortor lobortis lacus, at tincidunt diam sem vel magna. Proin rhoncus leo eget metus hendrerit, sit amet sollicitudin lectus rhoncus. Quisque eget ornare purus. Mauris sit amet risus at neque scelerisque congue ut in dolor. Nullam fermentum vitae arcu sed laoreet."
                },
                new Post
                {
                    PostID = 4,
                    UserId = userIds[1],
                    PostDatePosted = DateTime.Now,
                    PostText =
                        "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nulla tincidunt pharetra velit ut ullamcorper. Curabitur eget enim vitae purus convallis laoreet. Nulla facilisi. Sed rhoncus egestas nisl vitae hendrerit. Sed justo felis, vestibulum ac dignissim sed, rhoncus vel lorem. Sed viverra enim sed pulvinar egestas. Fusce sapien felis, ullamcorper vitae nunc sit amet, vulputate faucibus ex. Vivamus commodo dictum purus, non scelerisque mauris faucibus eu."
                },
                new Post
                {
                    PostID = 5,
                    UserId = userIds[1],
                    PostDatePosted = DateTime.Now,
                    PostText =
                        "Aliquam tempor ligula at diam feugiat, eu dignissim enim ultrices. Proin nec fringilla eros. Vivamus sollicitudin elit et risus laoreet aliquam. Integer scelerisque feugiat purus ac aliquet. Praesent sed mi lobortis, mattis metus at, sagittis eros. Quisque tempus porttitor iaculis. Morbi eget nunc felis."
                },
                new Post
                {
                    PostID = 6,
                    UserId = userIds[1],
                    PostDatePosted = DateTime.Now,
                    PostText =
                        "Vestibulum semper, enim id tincidunt tempor, metus tortor lobortis lacus, at tincidunt diam sem vel magna. Proin rhoncus leo eget metus hendrerit, sit amet sollicitudin lectus rhoncus. Quisque eget ornare purus. Mauris sit amet risus at neque scelerisque congue ut in dolor. Nullam fermentum vitae arcu sed laoreet."
                },

                new Post
                {
                    PostID = 7,
                    UserId = userIds[4],
                    PostDatePosted = DateTime.Now,
                    PostText =
                        "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nulla tincidunt pharetra velit ut ullamcorper. Curabitur eget enim vitae purus convallis laoreet. Nulla facilisi. Sed rhoncus egestas nisl vitae hendrerit. Sed justo felis, vestibulum ac dignissim sed, rhoncus vel lorem. Sed viverra enim sed pulvinar egestas. Fusce sapien felis, ullamcorper vitae nunc sit amet, vulputate faucibus ex. Vivamus commodo dictum purus, non scelerisque mauris faucibus eu."
                },
                new Post
                {
                    PostID = 8,
                    UserId = userIds[5],
                    PostDatePosted = DateTime.Now,
                    PostText =
                        "Aliquam tempor ligula at diam feugiat, eu dignissim enim ultrices. Proin nec fringilla eros. Vivamus sollicitudin elit et risus laoreet aliquam. Integer scelerisque feugiat purus ac aliquet. Praesent sed mi lobortis, mattis metus at, sagittis eros. Quisque tempus porttitor iaculis. Morbi eget nunc felis."
                },
                new Post
                {
                    PostID = 9,
                    UserId = userIds[6],
                    PostDatePosted = DateTime.Now,
                    PostText =
                        "Vestibulum semper, enim id tincidunt tempor, metus tortor lobortis lacus, at tincidunt diam sem vel magna. Proin rhoncus leo eget metus hendrerit, sit amet sollicitudin lectus rhoncus. Quisque eget ornare purus. Mauris sit amet risus at neque scelerisque congue ut in dolor. Nullam fermentum vitae arcu sed laoreet."
                },
                new Post
                {
                    PostID = 10,
                    UserId = userIds[14],
                    PostDatePosted = DateTime.Now,
                    PostText =
                        "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nulla tincidunt pharetra velit ut ullamcorper. Curabitur eget enim vitae purus convallis laoreet. Nulla facilisi. Sed rhoncus egestas nisl vitae hendrerit. Sed justo felis, vestibulum ac dignissim sed, rhoncus vel lorem. Sed viverra enim sed pulvinar egestas. Fusce sapien felis, ullamcorper vitae nunc sit amet, vulputate faucibus ex. Vivamus commodo dictum purus, non scelerisque mauris faucibus eu."
                },
                new Post
                {
                    PostID = 11,
                    UserId = userIds[2],
                    PostDatePosted = DateTime.Now,
                    PostText =
                        "Aliquam tempor ligula at diam feugiat, eu dignissim enim ultrices. Proin nec fringilla eros. Vivamus sollicitudin elit et risus laoreet aliquam. Integer scelerisque feugiat purus ac aliquet. Praesent sed mi lobortis, mattis metus at, sagittis eros. Quisque tempus porttitor iaculis. Morbi eget nunc felis."
                },
                new Post
                {
                    PostID = 12,
                    UserId = userIds[3],
                    PostDatePosted = DateTime.Now,
                    PostText =
                        "Vestibulum semper, enim id tincidunt tempor, metus tortor lobortis lacus, at tincidunt diam sem vel magna. Proin rhoncus leo eget metus hendrerit, sit amet sollicitudin lectus rhoncus. Quisque eget ornare purus. Mauris sit amet risus at neque scelerisque congue ut in dolor. Nullam fermentum vitae arcu sed laoreet."
                },
            };
            posts.ForEach(s => context.Posts.Add(s));
            var comment1 = new List<PostComments>
            {
                new PostComments
                {
                    PostComment = "Lorem ipsum dolor sit amet, consectetur adipiscing elit",
                    PostID = 1,
                    UserId = userIds[1],
                    PostCommentDate = DateTime.Now
                },
                new PostComments
                {
                    PostComment = "Lorem ipsum dolor sit amet, consectetur adipiscing elit",
                    PostID = 1,
                    UserId = userIds[2],
                    PostCommentDate = DateTime.Now
                },
                new PostComments
                {
                    PostComment = "Lorem ipsum dolor sit amet, consectetur adipiscing elit",
                    PostID = 1,
                    UserId = userIds[3],
                    PostCommentDate = DateTime.Now
                },
                new PostComments
                {
                    PostComment = "Lorem ipsum dolor sit amet, consectetur adipiscing elit",
                    PostID = 1,
                    UserId = userIds[1],
                    PostCommentDate = DateTime.Now
                },
                new PostComments
                {
                    PostComment = "Lorem ipsum dolor sit amet, consectetur adipiscing elit",
                    PostID = 1,
                    UserId = userIds[3],
                    PostCommentDate = DateTime.Now
                },
            };
            comment1.ForEach(s => context.PostComments.Add(s));
            var comment2 = new List<PostComments>
            {
                new PostComments
                {
                    PostComment = "Lorem ipsum dolor sit amet, consectetur adipiscing elit",
                    PostID = 2,
                    UserId = userIds[4],
                    PostCommentDate = DateTime.Now
                },
                new PostComments
                {
                    PostComment = "Lorem ipsum dolor sit amet, consectetur adipiscing elit",
                    PostID = 2,
                    UserId = userIds[5],
                    PostCommentDate = DateTime.Now
                },
            };
            comment2.ForEach(s => context.PostComments.Add(s));
            var comment3 = new List<PostComments>
            {
                new PostComments
                {
                    PostComment = "Lorem ipsum dolor sit amet, consectetur adipiscing elit",
                    PostID = 3,
                    UserId = userIds[3],
                    PostCommentDate = DateTime.Now
                },
                new PostComments
                {
                    PostComment = "Lorem ipsum dolor sit amet, consectetur adipiscing elit",
                    PostID = 3,
                    UserId = userIds[2],
                    PostCommentDate = DateTime.Now
                },
            };
            comment3.ForEach(s => context.PostComments.Add(s));
            var postPhotos = new List<PostPhotos>
            {
                new PostPhotos
                {
                    PostID = 1,
                    PostPhotosLocateBig = "../../images/post-images/1-big.jpg",
                    PostPhotosLocateSmall = ""
                },
                new PostPhotos
                {
                    PostID = 2,
                    PostPhotosLocateBig = "../../images/post-images/2-big.jpg",
                    PostPhotosLocateSmall = ""
                },
                new PostPhotos
                {
                    PostID = 3,
                    PostPhotosLocateBig = "../../images/post-images/3-big.jpg",
                    PostPhotosLocateSmall = ""
                },

                new PostPhotos
                {
                    PostID = 4,
                    PostPhotosLocateBig = "../../images/post-images/4-big.jpg",
                    PostPhotosLocateSmall = ""
                },
                new PostPhotos
                {
                    PostID = 5,
                    PostPhotosLocateBig = "../../images/post-images/5-big.jpg",
                    PostPhotosLocateSmall = ""
                },
                new PostPhotos
                {
                    PostID = 6,
                    PostPhotosLocateBig = "../../images/post-images/6-big.jpg",
                    PostPhotosLocateSmall = ""
                },

                new PostPhotos
                {
                    PostID = 7,
                    PostPhotosLocateBig = "../../images/post-images/7-big.jpg",
                    PostPhotosLocateSmall = ""
                },
                new PostPhotos
                {
                    PostID = 8,
                    PostPhotosLocateBig = "../../images/post-images/8-big.jpg",
                    PostPhotosLocateSmall = ""
                },
                new PostPhotos
                {
                    PostID = 9,
                    PostPhotosLocateBig = "../../images/post-images/9-big.jpg",
                    PostPhotosLocateSmall = ""
                },

                new PostPhotos
                {
                    PostID = 10,
                    PostPhotosLocateBig = "../../images/post-images/user-10-big.jpg",
                    PostPhotosLocateSmall = ""
                },
                new PostPhotos
                {
                    PostID = 11,
                    PostPhotosLocateBig = "../../images/post-images/user-11-big.jpg",
                    PostPhotosLocateSmall = ""
                },
                new PostPhotos
                {
                    PostID = 12,
                    PostPhotosLocateBig = "../../images/post-images/user-12-big.jpg",
                    PostPhotosLocateSmall = ""
                },
            };
            postPhotos.ForEach(s => context.PostPhotos.Add(s));
            var categories = new List<Categories>
            {
                new Categories {CategoryName = "Bilgisayar"}, //1
                new Categories {CategoryName = "Müzik"}, //2
                new Categories {CategoryName = "Kitap"}, //3
                new Categories {CategoryName = "Film"}, //4
                new Categories {CategoryName = "Spor"}, //5
                new Categories {CategoryName = "Yemek"}, //6
                new Categories {CategoryName = "Dizi"}, //7
                new Categories {CategoryName = "Futbol"}, //8
            };
            categories.ForEach(s => context.Categories.Add(s));

            var categoryItems = new List<CategoryItems>
            {
                new CategoryItems {CategoryID = 1, CategoryItemName = "Asp .net"}, //1
                new CategoryItems {CategoryID = 1, CategoryItemName = "Java"}, //2
                new CategoryItems {CategoryID = 1, CategoryItemName = "C#"}, //3
                new CategoryItems {CategoryID = 1, CategoryItemName = "Android"}, //4
                new CategoryItems {CategoryID = 2, CategoryItemName = "Metallica"}, //5
                new CategoryItems {CategoryID = 2, CategoryItemName = "Megadeath"}, //6
                new CategoryItems {CategoryID = 2, CategoryItemName = "Sýla"}, //7
                new CategoryItems {CategoryID = 2, CategoryItemName = "Tarkan"}, //8
                new CategoryItems {CategoryID = 3, CategoryItemName = "Kavgam"}, //9
                new CategoryItems {CategoryID = 3, CategoryItemName = "Sherlock"}, //10
                new CategoryItems {CategoryID = 3, CategoryItemName = "Suç ve Ceza"}, //11
                new CategoryItems {CategoryID = 4, CategoryItemName = "The Matrix"}, //12
                new CategoryItems {CategoryID = 4, CategoryItemName = "Lord of The Rings"}, //13
                new CategoryItems {CategoryID = 4, CategoryItemName = "Avengers"}, //14
                new CategoryItems {CategoryID = 4, CategoryItemName = "Interstellar"}, //15
                new CategoryItems {CategoryID = 5, CategoryItemName = "Basketbol"}, //16
                new CategoryItems {CategoryID = 5, CategoryItemName = "Futbol"}, //17
                new CategoryItems {CategoryID = 5, CategoryItemName = "Tekvando"}, //18
                new CategoryItems {CategoryID = 5, CategoryItemName = "Salon"}, //19
                new CategoryItems {CategoryID = 6, CategoryItemName = "Pizza"}, //20
                new CategoryItems {CategoryID = 6, CategoryItemName = "Adana"}, //21
                new CategoryItems {CategoryID = 6, CategoryItemName = "Lahmacun"}, //22
                new CategoryItems {CategoryID = 6, CategoryItemName = "Döner"}, //23
                new CategoryItems {CategoryID = 7, CategoryItemName = "Kurtlar Vadisi"}, //24
                new CategoryItems {CategoryID = 7, CategoryItemName = "Gotham"}, //25
                new CategoryItems {CategoryID = 7, CategoryItemName = "Supernatural"}, //26
                new CategoryItems {CategoryID = 8, CategoryItemName = "Galatasaray"}, //27
                new CategoryItems {CategoryID = 8, CategoryItemName = "Göztepe"}, //28
                new CategoryItems {CategoryID = 8, CategoryItemName = "Karþýyaka"}, //29
                new CategoryItems {CategoryID = 8, CategoryItemName = "Bursaspor"}, //30
            };
            categoryItems.ForEach(e => context.CategoryItems.Add(e));

            var userRatings = new List<UserRatings>
            {
                new UserRatings {UserId = userIds[1], CategoryItemID = 1, UserRating = 4},
                new UserRatings {UserId = userIds[1], CategoryItemID = 2, UserRating = 4},
                new UserRatings {UserId = userIds[1], CategoryItemID = 3, UserRating = 1},
                new UserRatings {UserId = userIds[1], CategoryItemID = 4, UserRating = 3},
                new UserRatings {UserId = userIds[1], CategoryItemID = 5, UserRating = 2},
                new UserRatings {UserId = userIds[1], CategoryItemID = 6, UserRating = 3},

                new UserRatings {UserId = userIds[2], CategoryItemID = 8, UserRating = 4},
                new UserRatings {UserId = userIds[2], CategoryItemID = 1, UserRating = 4},
                new UserRatings {UserId = userIds[2], CategoryItemID = 3, UserRating = 3},
                new UserRatings {UserId = userIds[2], CategoryItemID = 5, UserRating = 2},
                new UserRatings {UserId = userIds[2], CategoryItemID = 6, UserRating = 3},
                new UserRatings {UserId = userIds[3], CategoryItemID = 7, UserRating = 4},
                new UserRatings {UserId = userIds[3], CategoryItemID = 15, UserRating = 4},
                new UserRatings {UserId = userIds[3], CategoryItemID = 3, UserRating = 3},
                new UserRatings {UserId = userIds[3], CategoryItemID = 16, UserRating = 2},
                new UserRatings {UserId = userIds[3], CategoryItemID = 18, UserRating = 1},
                new UserRatings {UserId = userIds[4], CategoryItemID = 20, UserRating = 4},
                new UserRatings {UserId = userIds[4], CategoryItemID = 12, UserRating = 4},
                new UserRatings {UserId = userIds[4], CategoryItemID = 21, UserRating = 4},
                new UserRatings {UserId = userIds[4], CategoryItemID = 30, UserRating = 3},
                new UserRatings {UserId = userIds[4], CategoryItemID = 11, UserRating = 2},
                new UserRatings {UserId = userIds[5], CategoryItemID = 2, UserRating = 3},
                new UserRatings {UserId = userIds[5], CategoryItemID = 1, UserRating = 4},
                new UserRatings {UserId = userIds[5], CategoryItemID = 2, UserRating = 4},
                new UserRatings {UserId = userIds[5], CategoryItemID = 3, UserRating = 1},
                new UserRatings {UserId = userIds[5], CategoryItemID = 4, UserRating = 3},
                new UserRatings {UserId = userIds[6], CategoryItemID = 5, UserRating = 2},
                new UserRatings {UserId = userIds[6], CategoryItemID = 6, UserRating = 3},
                new UserRatings {UserId = userIds[6], CategoryItemID = 8, UserRating = 4},
                new UserRatings {UserId = userIds[6], CategoryItemID = 1, UserRating = 4},
                new UserRatings {UserId = userIds[6], CategoryItemID = 3, UserRating = 3},
                new UserRatings {UserId = userIds[6], CategoryItemID = 5, UserRating = 2},
                new UserRatings {UserId = userIds[7], CategoryItemID = 6, UserRating = 3},
                new UserRatings {UserId = userIds[7], CategoryItemID = 7, UserRating = 4},
                new UserRatings {UserId = userIds[7], CategoryItemID = 15, UserRating = 4},
                new UserRatings {UserId = userIds[7], CategoryItemID = 3, UserRating = 3},
                new UserRatings {UserId = userIds[7], CategoryItemID = 16, UserRating = 2},
                new UserRatings {UserId = userIds[7], CategoryItemID = 18, UserRating = 1},
                new UserRatings {UserId = userIds[8], CategoryItemID = 20, UserRating = 4},
                new UserRatings {UserId = userIds[8], CategoryItemID = 12, UserRating = 4},
                new UserRatings {UserId = userIds[8], CategoryItemID = 21, UserRating = 4},
                new UserRatings {UserId = userIds[8], CategoryItemID = 30, UserRating = 3},
                new UserRatings {UserId = userIds[8], CategoryItemID = 11, UserRating = 2},
                new UserRatings {UserId = userIds[8], CategoryItemID = 2, UserRating = 3},
                new UserRatings {UserId = userIds[9], CategoryItemID = 3, UserRating = 4},
                new UserRatings {UserId = userIds[9], CategoryItemID = 1, UserRating = 4},
                new UserRatings {UserId = userIds[9], CategoryItemID = 28, UserRating = 4},
                new UserRatings {UserId = userIds[9], CategoryItemID = 9, UserRating = 1},
                new UserRatings {UserId = userIds[9], CategoryItemID = 4, UserRating = 3},
                new UserRatings {UserId = userIds[9], CategoryItemID = 5, UserRating = 2},
                new UserRatings {UserId = userIds[14], CategoryItemID = 6, UserRating = 3},
                new UserRatings {UserId = userIds[14], CategoryItemID = 8, UserRating = 4},
                new UserRatings {UserId = userIds[14], CategoryItemID = 1, UserRating = 4},
                new UserRatings {UserId = userIds[13], CategoryItemID = 3, UserRating = 3},

                new UserRatings {UserId = userIds[10], CategoryItemID = 5, UserRating = 2},
                new UserRatings {UserId = userIds[10], CategoryItemID = 6, UserRating = 3},
                new UserRatings {UserId = userIds[10], CategoryItemID = 7, UserRating = 4},
                new UserRatings {UserId = userIds[11], CategoryItemID = 15, UserRating = 4},
                new UserRatings {UserId = userIds[11], CategoryItemID = 3, UserRating = 3},
                new UserRatings {UserId = userIds[11], CategoryItemID = 16, UserRating = 2},
                new UserRatings {UserId = userIds[12], CategoryItemID = 18, UserRating = 1},
                new UserRatings {UserId = userIds[12], CategoryItemID = 20, UserRating = 4},
                new UserRatings {UserId = userIds[12], CategoryItemID = 12, UserRating = 4},
                new UserRatings {UserId = userIds[12], CategoryItemID = 21, UserRating = 4},
                new UserRatings {UserId = userIds[13], CategoryItemID = 30, UserRating = 3},
                new UserRatings {UserId = userIds[13], CategoryItemID = 11, UserRating = 2},
                new UserRatings {UserId = userIds[13], CategoryItemID = 2, UserRating = 3},
                new UserRatings {UserId = userIds[14], CategoryItemID = 3, UserRating = 4},

            };
            userRatings.ForEach(e => context.UserRatings.Add(e));
            context.SaveChanges();
            context.Roles.Add(new Microsoft.AspNet.Identity.EntityFramework.IdentityRole {Name = "user"});

        }
    }
}
