using System;
using System.Collections.Generic;
using ScoutUp.Models;
namespace ScoutUp.DAL
{
    public class ScoutUpInitializer: System.Data.Entity.DropCreateDatabaseIfModelChanges<ScoutUpDB>
    {
        protected override void Seed(ScoutUpDB context)
        {
            //context.Database.ExecuteSqlCommand("DBCC CHECKIDENT ('User', RESEED, 1000000)");
            //context.Database.ExecuteSqlCommand("DBCC CHECKIDENT ('Post', RESEED, 10000000)");
            //context.Database.ExecuteSqlCommand("DBCC CHECKIDENT ('PostComments', RESEED, 100000000)");
            //context.SaveChanges();
        var users = new List<User> {
                new User {UserName="Burak",UserSurname="Portakal",UserPassword="1111",UserEmail="b@h.com",UserCity="Izmir",UserAbout="Lorem ipsum",UserBirthDate=DateTime.Parse("1990-09-16"),UserProfilePhoto = "../../images/post-images/user-1-tumbnail.jpg"},
                new User {UserName="Kadir",UserSurname="Kanmaz",UserPassword="1111",UserEmail="kadir@h.com",UserCity="Denizli",UserAbout="Lorem ipsum",UserBirthDate=DateTime.Parse("1990-09-16"),UserProfilePhoto = "../../images/post-images/user-2-tumbnail.jpg" },
                new User {UserName="Onur",UserSurname="Sal",UserPassword="1111",UserEmail="onur@h.com",UserCity="Bursa",UserAbout="Lorem ipsum",UserBirthDate=DateTime.Parse("1990-09-16"),UserProfilePhoto = "../../images/post-images/user-3-tumbnail.jpg" },
                new User {UserName="Tayfun",UserSurname="Erturul",UserPassword="1111",UserEmail="tayfun@h.com",UserCity="Istanbul",UserAbout="Lorem ipsum",UserBirthDate=DateTime.Parse("1990-09-16"),UserProfilePhoto = "../../images/post-images/user-4-tumbnail.jpg" },
                new User {UserName="Mehmet",UserSurname="Akgül",UserPassword="1111",UserEmail="mehmet@h.com",UserCity="Konya",UserAbout="Lorem ipsum",UserBirthDate=DateTime.Parse("1990-09-16"),UserProfilePhoto = "../../images/post-images/user-5-tumbnail.jpg" },
                new User {UserName="Esra",UserSurname="Akgül",UserPassword="1111",UserEmail="esra@h.com",UserCity="Eskisehir",UserAbout="Lorem ipsum",UserBirthDate=DateTime.Parse("1990-09-16"),UserProfilePhoto = "../../images/post-images/user-6-tumbnail.jpg" },
                new User {UserName="Adem",UserSurname="Akgül",UserPassword="1111",UserEmail="adem@h.com",UserCity="Konya",UserAbout="Lorem ipsum",UserBirthDate=DateTime.Parse("1990-09-16"),UserProfilePhoto = "../../images/post-images/user-7-tumbnail.jpg" },
                new User {UserName="Sıtkı",UserSurname="Portakal",UserPassword="1111",UserEmail="sitki@h.com",UserCity="Izmir",UserAbout="Lorem ipsum",UserBirthDate=DateTime.Parse("1990-09-16"),UserProfilePhoto = "../../images/post-images/user-8-tumbnail.jpg" },
                new User {UserName="Hüseyin",UserSurname="Portakal",UserPassword="1111",UserEmail="huseyin@h.com",UserCity="Usak",UserAbout="Lorem ipsum",UserBirthDate=DateTime.Parse("1990-09-16"),UserProfilePhoto = "../../images/post-images/user-9-tumbnail.jpg" },
                new User {UserName="Lütfiye",UserSurname="Portakal",UserPassword="1111",UserEmail="lutfiye@h.com",UserCity="Izmir",UserAbout="Lorem ipsum",UserBirthDate=DateTime.Parse("1990-09-16") ,UserProfilePhoto = "../../images/post-images/user-10-tumbnail.jpg"},
                new User {UserName="Emine",UserSurname="Amil",UserPassword="1111",UserEmail="emine@h.com",UserCity="Izmir",UserAbout="Lorem ipsum",UserBirthDate=DateTime.Parse("1990-09-16"),UserProfilePhoto = "../../images/post-images/user-11-tumbnail.jpg" },
                new User {UserName="Ali",UserSurname="Amil",UserPassword="1111",UserEmail="ali@h.com",UserCity="Izmir",UserAbout="Lorem ipsum",UserBirthDate=DateTime.Parse("1990-09-16"),UserProfilePhoto = "../../images/post-images/user-12-tumbnail.jpg" },
                new User {UserName="Mustafa",UserSurname="Amil",UserPassword="1111",UserEmail="mustafa@h.com",UserCity="Izmir",UserAbout="Lorem ipsum",UserBirthDate=DateTime.Parse("1990-09-16"),UserProfilePhoto = "../../images/post-images/user-13-tumbnail.jpg" },
                new User {UserName="Fatma",UserSurname="Amil",UserPassword="1111",UserEmail="fatma@h.com",UserCity="Izmir",UserAbout="Lorem ipsum",UserBirthDate=DateTime.Parse("1990-09-16"),UserProfilePhoto = "../../images/post-images/user-14-tumbnail.jpg" },

            };
            var userPhotos = new List<UserPhotos>
            {
                new UserPhotos { UserID = 1,UserPhotoBig = "user-1-big.jpg",UserPhotoSmall = "../../images/post-images/user-1-tumbnail.jpg",IsDeleted = false},
                new UserPhotos { UserID = 2,UserPhotoBig = "user-2-big.jpg",UserPhotoSmall = "../../images/post-images/user-2-tumbnail.jpg",IsDeleted = false},
                new UserPhotos { UserID = 3,UserPhotoBig = "user-3-big.jpg",UserPhotoSmall = "../../images/post-images/user-3-tumbnail.jpg",IsDeleted = false},
                new UserPhotos { UserID = 4,UserPhotoBig = "user-4-big.jpg",UserPhotoSmall = "../../images/post-images/user-4-tumbnail.jpg",IsDeleted = false},
                new UserPhotos { UserID = 5,UserPhotoBig = "user-5-big.jpg",UserPhotoSmall = "../../images/post-images/user-5-tumbnail.jpg",IsDeleted = false},
                new UserPhotos { UserID = 6,UserPhotoBig = "user-6-big.jpg",UserPhotoSmall = "../../images/post-images/user-6-tumbnail.jpg",IsDeleted = false},
                new UserPhotos { UserID = 7,UserPhotoBig = "user-7-big.jpg",UserPhotoSmall = "../../images/post-images/user-7-tumbnail.jpg",IsDeleted = false},
                new UserPhotos { UserID = 8,UserPhotoBig = "user-8-big.jpg",UserPhotoSmall = "../../images/post-images/user-8-tumbnail.jpg",IsDeleted = false},
                new UserPhotos { UserID = 9,UserPhotoBig = "user-9-big.jpg",UserPhotoSmall = "../../images/post-images/user-9-tumbnail.jpg",IsDeleted = false},
                new UserPhotos { UserID = 10,UserPhotoBig = "user-10-big.jpg",UserPhotoSmall = "../../images/post-images/user-10-tumbnail.jpg",IsDeleted = false},
                new UserPhotos { UserID = 11,UserPhotoBig = "user-11-big.jpg",UserPhotoSmall = "../../images/post-images/user-11-tumbnail.jpg",IsDeleted = false},
                new UserPhotos { UserID = 12,UserPhotoBig = "user-12-big.jpg",UserPhotoSmall = "../../images/post-images/user-12-tumbnail.jpg",IsDeleted = false},
                new UserPhotos { UserID = 13,UserPhotoBig = "user-13-big.jpg",UserPhotoSmall = "../../images/post-images/user-13-tumbnail.jpg",IsDeleted = false},
                new UserPhotos { UserID = 14,UserPhotoBig = "user-14-big.jpg",UserPhotoSmall = "../../images/post-images/user-14-tumbnail.jpg",IsDeleted = false},
            };
            users.ForEach(s => context.Users.Add(s));
            context.SaveChanges();
            var hobbies = new List<Hobbies> {
                new Hobbies { HobbiesName="Bilgisayar" },//1
                new Hobbies {HobbiesName="Spor" },//2
                new Hobbies { HobbiesName="Müzik" },//3
                new Hobbies {HobbiesName="Futbol" },//4
                new Hobbies { HobbiesName="Kodlama" },//5
                new Hobbies {HobbiesName="Java" },//6
                new Hobbies { HobbiesName="Asp" },//7
                new Hobbies {HobbiesName="Gitar" },//8
                new Hobbies { HobbiesName="Telefon" },//9
                new Hobbies {HobbiesName="Sosyal" }//10

            };
            hobbies.ForEach(s => context.Hobbies.Add(s));
            context.SaveChanges();
            var userHobbies = new List<UserHobbies>
            {
                new UserHobbies {HobbiesID = 1,UserID = 1},
                new UserHobbies {HobbiesID = 1,UserID = 2},
                new UserHobbies {HobbiesID = 1,UserID = 4},
                new UserHobbies {HobbiesID = 1,UserID = 5},
                new UserHobbies {HobbiesID = 1,UserID = 1},
                new UserHobbies {HobbiesID = 1,UserID = 1},
                new UserHobbies {HobbiesID = 1,UserID = 1},
                new UserHobbies {HobbiesID = 1,UserID = 1},
                new UserHobbies {HobbiesID = 1,UserID = 1},
                new UserHobbies {HobbiesID = 1,UserID = 1},
                new UserHobbies {HobbiesID = 1,UserID = 1},
                new UserHobbies {HobbiesID = 1,UserID = 1},
                new UserHobbies {HobbiesID = 1,UserID = 1},
                new UserHobbies {HobbiesID = 1,UserID = 1},
                new UserHobbies {HobbiesID = 1,UserID = 1},
                new UserHobbies {HobbiesID = 1,UserID = 1},
                new UserHobbies {HobbiesID = 1,UserID = 1},
                new UserHobbies {HobbiesID = 1,UserID = 1},
                new UserHobbies {HobbiesID = 1,UserID = 1},
                new UserHobbies {HobbiesID = 1,UserID = 1},
                new UserHobbies {HobbiesID = 1,UserID = 1},
                new UserHobbies {HobbiesID = 1,UserID = 1},
            };
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
                new Post {PostID = 1,UserID = 7,PostDatePosted = DateTime.Now,PostText = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nulla tincidunt pharetra velit ut ullamcorper. Curabitur eget enim vitae purus convallis laoreet. Nulla facilisi. Sed rhoncus egestas nisl vitae hendrerit. Sed justo felis, vestibulum ac dignissim sed, rhoncus vel lorem. Sed viverra enim sed pulvinar egestas. Fusce sapien felis, ullamcorper vitae nunc sit amet, vulputate faucibus ex. Vivamus commodo dictum purus, non scelerisque mauris faucibus eu."},
                new Post {PostID = 2,UserID = 7,PostDatePosted = DateTime.Now,PostText = "Aliquam tempor ligula at diam feugiat, eu dignissim enim ultrices. Proin nec fringilla eros. Vivamus sollicitudin elit et risus laoreet aliquam. Integer scelerisque feugiat purus ac aliquet. Praesent sed mi lobortis, mattis metus at, sagittis eros. Quisque tempus porttitor iaculis. Morbi eget nunc felis."},
                new Post {PostID = 3,UserID = 7,PostDatePosted = DateTime.Now,PostText = "Vestibulum semper, enim id tincidunt tempor, metus tortor lobortis lacus, at tincidunt diam sem vel magna. Proin rhoncus leo eget metus hendrerit, sit amet sollicitudin lectus rhoncus. Quisque eget ornare purus. Mauris sit amet risus at neque scelerisque congue ut in dolor. Nullam fermentum vitae arcu sed laoreet."},
                new Post {PostID = 4,UserID = 1,PostDatePosted = DateTime.Now,PostText = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nulla tincidunt pharetra velit ut ullamcorper. Curabitur eget enim vitae purus convallis laoreet. Nulla facilisi. Sed rhoncus egestas nisl vitae hendrerit. Sed justo felis, vestibulum ac dignissim sed, rhoncus vel lorem. Sed viverra enim sed pulvinar egestas. Fusce sapien felis, ullamcorper vitae nunc sit amet, vulputate faucibus ex. Vivamus commodo dictum purus, non scelerisque mauris faucibus eu."},
                new Post {PostID = 5,UserID = 1,PostDatePosted = DateTime.Now,PostText = "Aliquam tempor ligula at diam feugiat, eu dignissim enim ultrices. Proin nec fringilla eros. Vivamus sollicitudin elit et risus laoreet aliquam. Integer scelerisque feugiat purus ac aliquet. Praesent sed mi lobortis, mattis metus at, sagittis eros. Quisque tempus porttitor iaculis. Morbi eget nunc felis."},
                new Post {PostID = 6,UserID = 1,PostDatePosted = DateTime.Now,PostText = "Vestibulum semper, enim id tincidunt tempor, metus tortor lobortis lacus, at tincidunt diam sem vel magna. Proin rhoncus leo eget metus hendrerit, sit amet sollicitudin lectus rhoncus. Quisque eget ornare purus. Mauris sit amet risus at neque scelerisque congue ut in dolor. Nullam fermentum vitae arcu sed laoreet."},

                new Post {PostID = 7,UserID = 7,PostDatePosted = DateTime.Now,PostText = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nulla tincidunt pharetra velit ut ullamcorper. Curabitur eget enim vitae purus convallis laoreet. Nulla facilisi. Sed rhoncus egestas nisl vitae hendrerit. Sed justo felis, vestibulum ac dignissim sed, rhoncus vel lorem. Sed viverra enim sed pulvinar egestas. Fusce sapien felis, ullamcorper vitae nunc sit amet, vulputate faucibus ex. Vivamus commodo dictum purus, non scelerisque mauris faucibus eu."},
                new Post {PostID = 8,UserID = 7,PostDatePosted = DateTime.Now,PostText = "Aliquam tempor ligula at diam feugiat, eu dignissim enim ultrices. Proin nec fringilla eros. Vivamus sollicitudin elit et risus laoreet aliquam. Integer scelerisque feugiat purus ac aliquet. Praesent sed mi lobortis, mattis metus at, sagittis eros. Quisque tempus porttitor iaculis. Morbi eget nunc felis."},
                new Post {PostID = 9,UserID = 7,PostDatePosted = DateTime.Now,PostText = "Vestibulum semper, enim id tincidunt tempor, metus tortor lobortis lacus, at tincidunt diam sem vel magna. Proin rhoncus leo eget metus hendrerit, sit amet sollicitudin lectus rhoncus. Quisque eget ornare purus. Mauris sit amet risus at neque scelerisque congue ut in dolor. Nullam fermentum vitae arcu sed laoreet."},
                new Post {PostID = 10,UserID = 1,PostDatePosted = DateTime.Now,PostText = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nulla tincidunt pharetra velit ut ullamcorper. Curabitur eget enim vitae purus convallis laoreet. Nulla facilisi. Sed rhoncus egestas nisl vitae hendrerit. Sed justo felis, vestibulum ac dignissim sed, rhoncus vel lorem. Sed viverra enim sed pulvinar egestas. Fusce sapien felis, ullamcorper vitae nunc sit amet, vulputate faucibus ex. Vivamus commodo dictum purus, non scelerisque mauris faucibus eu."},
                new Post {PostID = 11,UserID = 1,PostDatePosted = DateTime.Now,PostText = "Aliquam tempor ligula at diam feugiat, eu dignissim enim ultrices. Proin nec fringilla eros. Vivamus sollicitudin elit et risus laoreet aliquam. Integer scelerisque feugiat purus ac aliquet. Praesent sed mi lobortis, mattis metus at, sagittis eros. Quisque tempus porttitor iaculis. Morbi eget nunc felis."},
                new Post {PostID = 12,UserID = 1,PostDatePosted = DateTime.Now,PostText = "Vestibulum semper, enim id tincidunt tempor, metus tortor lobortis lacus, at tincidunt diam sem vel magna. Proin rhoncus leo eget metus hendrerit, sit amet sollicitudin lectus rhoncus. Quisque eget ornare purus. Mauris sit amet risus at neque scelerisque congue ut in dolor. Nullam fermentum vitae arcu sed laoreet."},
            };
            posts.ForEach(s => context.Posts.Add(s));
            var comment1 =new List<PostComments>
            {
                new PostComments {PostComment = "Lorem ipsum dolor sit amet, consectetur adipiscing elit", PostID = 1, UserID = 1,PostCommentDate = DateTime.Now},
                new PostComments {PostComment = "Lorem ipsum dolor sit amet, consectetur adipiscing elit", PostID = 1, UserID = 2,PostCommentDate = DateTime.Now},
                new PostComments {PostComment = "Lorem ipsum dolor sit amet, consectetur adipiscing elit", PostID = 1, UserID = 3,PostCommentDate = DateTime.Now},
                new PostComments {PostComment = "Lorem ipsum dolor sit amet, consectetur adipiscing elit", PostID = 1, UserID = 1,PostCommentDate = DateTime.Now},
                new PostComments {PostComment = "Lorem ipsum dolor sit amet, consectetur adipiscing elit", PostID = 1, UserID = 3,PostCommentDate = DateTime.Now},
            };
            comment1.ForEach(s => context.PostComments.Add(s));
            var comment2 = new List<PostComments>
            {
                new PostComments {PostComment = "Lorem ipsum dolor sit amet, consectetur adipiscing elit", PostID = 2, UserID = 4,PostCommentDate = DateTime.Now},
                new PostComments {PostComment = "Lorem ipsum dolor sit amet, consectetur adipiscing elit", PostID = 2, UserID = 5,PostCommentDate = DateTime.Now},
            };
            comment2.ForEach(s => context.PostComments.Add(s));
            var comment3 = new List<PostComments>
            {
                new PostComments {PostComment = "Lorem ipsum dolor sit amet, consectetur adipiscing elit", PostID = 3, UserID = 3,PostCommentDate = DateTime.Now},
                new PostComments {PostComment = "Lorem ipsum dolor sit amet, consectetur adipiscing elit", PostID = 3, UserID = 2,PostCommentDate = DateTime.Now},
            };
            comment3.ForEach(s => context.PostComments.Add(s));
            var postPhotos = new List<PostPhotos>
            {
                new PostPhotos { PostID = 1,PostPhotosLocateBig = "../../images/post-images/1-big.jpg",PostPhotosLocateSmall = ""},
                new PostPhotos { PostID = 2,PostPhotosLocateBig = "../../images/post-images/2-big.jpg",PostPhotosLocateSmall = ""},
                new PostPhotos { PostID = 3,PostPhotosLocateBig = "../../images/post-images/3-big.jpg",PostPhotosLocateSmall = ""},

                new PostPhotos { PostID = 4,PostPhotosLocateBig = "../../images/post-images/4-big.jpg",PostPhotosLocateSmall = ""},
                new PostPhotos { PostID = 5,PostPhotosLocateBig = "../../images/post-images/5-big.jpg",PostPhotosLocateSmall = ""},
                new PostPhotos { PostID = 6,PostPhotosLocateBig = "../../images/post-images/6-big.jpg",PostPhotosLocateSmall = ""},

                new PostPhotos { PostID = 7,PostPhotosLocateBig = "../../images/post-images/7-big.jpg",PostPhotosLocateSmall = ""},
                new PostPhotos { PostID = 8,PostPhotosLocateBig = "../../images/post-images/8-big.jpg",PostPhotosLocateSmall = ""},
                new PostPhotos { PostID = 9,PostPhotosLocateBig = "../../images/post-images/9-big.jpg",PostPhotosLocateSmall = ""},

                new PostPhotos { PostID = 10,PostPhotosLocateBig = "../../images/post-images/user-10-big.jpg",PostPhotosLocateSmall = ""},
                new PostPhotos { PostID = 11,PostPhotosLocateBig = "../../images/post-images/user-11-big.jpg",PostPhotosLocateSmall = ""},
                new PostPhotos { PostID = 12,PostPhotosLocateBig = "../../images/post-images/user-12-big.jpg",PostPhotosLocateSmall = ""},
            };
            postPhotos.ForEach(s => context.PostPhotos.Add(s));
            var categories = new List<Categories>
            {
                new Categories {CategoryName = "Bilgisayar" },//1
                new Categories {CategoryName = "Müzik" },//2
                new Categories {CategoryName = "Kitap" },//3
                new Categories {CategoryName = "Film" },//4
                new Categories {CategoryName = "Spor" },//5
                new Categories {CategoryName = "Yemek" },//6
                new Categories {CategoryName = "Dizi" },//7
                new Categories {CategoryName = "Futbol" },//8
            };
            categories.ForEach(s => context.Categories.Add(s));

            var categoryItems = new List<CategoryItems>
            {
                new CategoryItems {CategoryID = 1,CategoryItemName = "Asp .net"},//1
                new CategoryItems {CategoryID = 1,CategoryItemName = "Java"},//2
                new CategoryItems {CategoryID = 1,CategoryItemName = "C#"},//3
                new CategoryItems {CategoryID = 1,CategoryItemName = "Android"},//4
                new CategoryItems {CategoryID = 2,CategoryItemName = "Metallica"},//5
                new CategoryItems {CategoryID = 2,CategoryItemName = "Megadeath"},//6
                new CategoryItems {CategoryID = 2,CategoryItemName = "Sıla"},//7
                new CategoryItems {CategoryID = 2,CategoryItemName = "Tarkan"},//8
                new CategoryItems {CategoryID = 3,CategoryItemName = "Kavgam"},//9
                new CategoryItems {CategoryID = 3,CategoryItemName = "Sherlock"},//10
                new CategoryItems {CategoryID = 3,CategoryItemName = "Suç ve Ceza"},//11
                new CategoryItems {CategoryID = 4,CategoryItemName = "The Matrix"},//12
                new CategoryItems {CategoryID = 4,CategoryItemName = "Lord of The Rings"},//13
                new CategoryItems {CategoryID = 4,CategoryItemName = "Avengers"},//14
                new CategoryItems {CategoryID = 4,CategoryItemName = "Interstellar"},//15
                new CategoryItems {CategoryID = 5,CategoryItemName = "Basketbol"},//16
                new CategoryItems {CategoryID = 5,CategoryItemName = "Futbol"},//17
                new CategoryItems {CategoryID = 5,CategoryItemName = "Tekvando"},//18
                new CategoryItems {CategoryID = 5,CategoryItemName = "Salon"},//19
                new CategoryItems {CategoryID = 6,CategoryItemName = "Pizza"},//20
                new CategoryItems {CategoryID = 6,CategoryItemName = "Adana"},//21
                new CategoryItems {CategoryID = 6,CategoryItemName = "Lahmacun"},//22
                new CategoryItems {CategoryID = 6,CategoryItemName = "Döner"},//23
                new CategoryItems {CategoryID = 7,CategoryItemName = "Kurtlar Vadisi"},//24
                new CategoryItems {CategoryID = 7,CategoryItemName = "Gotham"},//25
                new CategoryItems {CategoryID = 7,CategoryItemName = "Supernatural"},//26
                new CategoryItems {CategoryID = 8,CategoryItemName = "Galatasaray"},//27
                new CategoryItems {CategoryID = 8,CategoryItemName = "Göztepe"},//28
                new CategoryItems {CategoryID = 8,CategoryItemName = "Karşıyaka"},//29
                new CategoryItems {CategoryID = 8,CategoryItemName = "Bursaspor"},//30
            };
            categoryItems.ForEach(e => context.CategoryItems.Add(e));

            var userRatings = new List<UserRatings>
            {
                new UserRatings { UserID = 1,CategoryItemID = 1,UserRating =4},
                new UserRatings { UserID = 1,CategoryItemID = 2,UserRating =4},
                new UserRatings { UserID = 1,CategoryItemID = 3,UserRating =1},
                new UserRatings { UserID = 1,CategoryItemID = 4,UserRating =3},
                new UserRatings { UserID = 1,CategoryItemID = 5,UserRating =2},
                new UserRatings { UserID = 1,CategoryItemID = 6,UserRating =3},
                new UserRatings { UserID = 2,CategoryItemID = 8,UserRating =4},
                new UserRatings { UserID = 2,CategoryItemID = 1,UserRating =4},
                new UserRatings { UserID = 2,CategoryItemID = 3,UserRating =3},
                new UserRatings { UserID = 2,CategoryItemID = 5,UserRating =2},
                new UserRatings { UserID = 2,CategoryItemID = 6,UserRating =3},
                new UserRatings { UserID = 2,CategoryItemID = 7,UserRating =4},
                new UserRatings { UserID = 2,CategoryItemID = 15,UserRating =4},

                new UserRatings { UserID = 3,CategoryItemID = 3,UserRating =3},
                new UserRatings { UserID = 3,CategoryItemID = 16,UserRating =2},
                new UserRatings { UserID = 3,CategoryItemID = 18,UserRating =1},
                new UserRatings { UserID = 3,CategoryItemID = 20,UserRating =4},
                new UserRatings { UserID = 3,CategoryItemID = 12,UserRating =4},
                new UserRatings { UserID = 3,CategoryItemID = 21,UserRating =4},
                new UserRatings { UserID = 3,CategoryItemID = 30,UserRating =3},
                new UserRatings { UserID = 3,CategoryItemID = 11,UserRating =2},
                new UserRatings { UserID = 3,CategoryItemID = 2,UserRating =3},

                new UserRatings { UserID = 4,CategoryItemID = 1,UserRating =4},
                new UserRatings { UserID = 4,CategoryItemID = 2,UserRating =4},
                new UserRatings { UserID = 4,CategoryItemID = 3,UserRating =1},
                new UserRatings { UserID = 4,CategoryItemID = 4,UserRating =3},
                new UserRatings { UserID = 4,CategoryItemID = 5,UserRating =2},
                new UserRatings { UserID = 4,CategoryItemID = 6,UserRating =3},

                new UserRatings { UserID = 5,CategoryItemID = 8,UserRating =4},
                new UserRatings { UserID = 5,CategoryItemID = 1,UserRating =4},
                new UserRatings { UserID = 5,CategoryItemID = 3,UserRating =3},
                new UserRatings { UserID = 5,CategoryItemID = 5,UserRating =2},
                new UserRatings { UserID = 5,CategoryItemID = 6,UserRating =3},

                new UserRatings { UserID = 6,CategoryItemID = 7,UserRating =4},
                new UserRatings { UserID = 6,CategoryItemID = 15,UserRating =4},
                new UserRatings { UserID = 6,CategoryItemID = 3,UserRating =3},
                new UserRatings { UserID = 6,CategoryItemID = 16,UserRating =2},
                new UserRatings { UserID = 6,CategoryItemID = 18,UserRating =1},

                new UserRatings { UserID = 7,CategoryItemID = 20,UserRating =4},
                new UserRatings { UserID = 7,CategoryItemID = 12,UserRating =4},
                new UserRatings { UserID = 7,CategoryItemID = 21,UserRating =4},
                new UserRatings { UserID = 7,CategoryItemID = 30,UserRating =3},
                new UserRatings { UserID = 7,CategoryItemID = 11,UserRating =2},

                new UserRatings { UserID = 8,CategoryItemID = 2,UserRating =3},
                new UserRatings { UserID = 8,CategoryItemID = 3,UserRating =4},
                new UserRatings { UserID = 8,CategoryItemID = 1,UserRating =4},
                new UserRatings { UserID = 8,CategoryItemID = 28,UserRating =4},
                new UserRatings { UserID = 8,CategoryItemID = 9,UserRating =1},

                new UserRatings { UserID = 8,CategoryItemID = 4,UserRating =3},
                new UserRatings { UserID = 9,CategoryItemID = 5,UserRating =2},
                new UserRatings { UserID = 9,CategoryItemID = 6,UserRating =3},
                new UserRatings { UserID = 9,CategoryItemID = 8,UserRating =4},
                new UserRatings { UserID = 9,CategoryItemID = 1,UserRating =4},
                new UserRatings { UserID = 9,CategoryItemID = 3,UserRating =3},

                new UserRatings { UserID = 10,CategoryItemID = 5,UserRating =2},
                new UserRatings { UserID = 10,CategoryItemID = 6,UserRating =3},
                new UserRatings { UserID = 10,CategoryItemID = 7,UserRating =4},

                new UserRatings { UserID = 11,CategoryItemID = 15,UserRating =4},
                new UserRatings { UserID = 11,CategoryItemID = 3,UserRating =3},
                new UserRatings { UserID = 11,CategoryItemID = 16,UserRating =2},
                new UserRatings { UserID = 11,CategoryItemID = 18,UserRating =1},

                new UserRatings { UserID = 12,CategoryItemID = 20,UserRating =4},
                new UserRatings { UserID = 12,CategoryItemID = 12,UserRating =4},

                new UserRatings { UserID = 13,CategoryItemID = 21,UserRating =4},
                new UserRatings { UserID = 13,CategoryItemID = 30,UserRating =3},

                new UserRatings { UserID = 14,CategoryItemID = 11,UserRating =2},
                new UserRatings { UserID = 14,CategoryItemID = 2,UserRating =3},
                new UserRatings { UserID = 14,CategoryItemID = 3,UserRating =4},

            };
            userRatings.ForEach(e => context.UserRatings.Add(e));
            context.SaveChanges();
            context.Roles.Add(new Microsoft.AspNet.Identity.EntityFramework.IdentityRole { Name = "user" });
        }
    }
}