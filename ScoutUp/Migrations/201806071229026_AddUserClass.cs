namespace ScoutUp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUserClass : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        CategoryID = c.Int(nullable: false, identity: true),
                        CategoryName = c.String(),
                    })
                .PrimaryKey(t => t.CategoryID);
            
            CreateTable(
                "dbo.CategoryItems",
                c => new
                    {
                        CategoryItemID = c.Int(nullable: false, identity: true),
                        CategoryID = c.Int(nullable: false),
                        CategoryItemName = c.String(),
                        CategoryItemPhoto = c.String(),
                    })
                .PrimaryKey(t => t.CategoryItemID)
                .ForeignKey("dbo.Categories", t => t.CategoryID, cascadeDelete: true)
                .Index(t => t.CategoryID);
            
            CreateTable(
                "dbo.UserRatings",
                c => new
                    {
                        UserRatingsID = c.Int(nullable: false, identity: true),
                        UserId = c.String(maxLength: 128),
                        CategoryItemID = c.Int(nullable: false),
                        UserRating = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.UserRatingsID)
                .ForeignKey("dbo.CategoryItems", t => t.CategoryItemID, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.CategoryItemID);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        UserFirstName = c.String(),
                        UserSurname = c.String(),
                        UserCity = c.String(),
                        UserAbout = c.String(),
                        UserBirthDate = c.DateTime(nullable: false),
                        UserGender = c.Int(nullable: false),
                        UserProfilePhoto = c.String(),
                        IsFirstLogin = c.Boolean(nullable: false),
                        Email = c.String(),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Chat",
                c => new
                    {
                        ChatId = c.Int(nullable: false, identity: true),
                        UserId = c.String(maxLength: 128),
                        OtherUserId = c.String(),
                    })
                .PrimaryKey(t => t.ChatId)
                .ForeignKey("dbo.User", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.ChatMessages",
                c => new
                    {
                        ChatMessagesId = c.Int(nullable: false, identity: true),
                        ChatId = c.Int(nullable: false),
                        ChatMessageText = c.String(),
                        ChatMessagesSendDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ChatMessagesId)
                .ForeignKey("dbo.Chat", t => t.ChatId, cascadeDelete: true)
                .Index(t => t.ChatId);
            
            CreateTable(
                "dbo.IdentityUserClaim",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.IdentityUserLogin",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        LoginProvider = c.String(),
                        ProviderKey = c.String(),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.User", t => t.User_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.IdentityUserRole",
                c => new
                    {
                        RoleId = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                        IdentityRole_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => new { t.RoleId, t.UserId })
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.IdentityRole", t => t.IdentityRole_Id)
                .Index(t => t.UserId)
                .Index(t => t.IdentityRole_Id);
            
            CreateTable(
                "dbo.UserFollow",
                c => new
                    {
                        UserFollowID = c.Int(nullable: false, identity: true),
                        UserId = c.String(maxLength: 128),
                        UserBeingFollowedUserId = c.String(),
                        IsFollowing = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.UserFollowID)
                .ForeignKey("dbo.User", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.UserHobbies",
                c => new
                    {
                        UserHobbiesID = c.Int(nullable: false, identity: true),
                        UserId = c.String(maxLength: 128),
                        HobbiesID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UserHobbiesID)
                .ForeignKey("dbo.Hobbies", t => t.HobbiesID, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.HobbiesID);
            
            CreateTable(
                "dbo.Hobbies",
                c => new
                    {
                        HobbiesID = c.Int(nullable: false, identity: true),
                        HobbiesName = c.String(),
                    })
                .PrimaryKey(t => t.HobbiesID);
            
            CreateTable(
                "dbo.UserNotifications",
                c => new
                    {
                        UserNotificationsID = c.Int(nullable: false, identity: true),
                        UserId = c.String(maxLength: 128),
                        UserNotificationsMessage = c.String(),
                        UserNotificationsDate = c.DateTime(nullable: false),
                        UserNotificationsRead = c.Boolean(nullable: false),
                        NotificationLink = c.String(),
                    })
                .PrimaryKey(t => t.UserNotificationsID)
                .ForeignKey("dbo.User", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.UserPhotos",
                c => new
                    {
                        UserPhotosID = c.Int(nullable: false, identity: true),
                        UserId = c.String(maxLength: 128),
                        UserPhotoSmall = c.String(),
                        UserPhotoBig = c.String(),
                    })
                .PrimaryKey(t => t.UserPhotosID)
                .ForeignKey("dbo.User", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Post",
                c => new
                    {
                        PostID = c.Int(nullable: false, identity: true),
                        UserId = c.String(maxLength: 128),
                        PostText = c.String(),
                        PostDatePosted = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.PostID)
                .ForeignKey("dbo.User", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.PostComments",
                c => new
                    {
                        PostCommentsID = c.Int(nullable: false, identity: true),
                        PostID = c.Int(nullable: false),
                        UserId = c.String(),
                        PostComment = c.String(),
                        PostCommentDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.PostCommentsID)
                .ForeignKey("dbo.Post", t => t.PostID, cascadeDelete: true)
                .Index(t => t.PostID);
            
            CreateTable(
                "dbo.PostLikes",
                c => new
                    {
                        PostLikesID = c.Int(nullable: false, identity: true),
                        PostID = c.Int(nullable: false),
                        UserId = c.String(),
                        IsLiked = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.PostLikesID)
                .ForeignKey("dbo.Post", t => t.PostID, cascadeDelete: true)
                .Index(t => t.PostID);
            
            CreateTable(
                "dbo.PostPhotos",
                c => new
                    {
                        PostPhotosID = c.Int(nullable: false, identity: true),
                        PostID = c.Int(nullable: false),
                        PostPhotosLocateSmall = c.String(),
                        PostPhotosLocateBig = c.String(),
                    })
                .PrimaryKey(t => t.PostPhotosID)
                .ForeignKey("dbo.Post", t => t.PostID, cascadeDelete: true)
                .Index(t => t.PostID);
            
            CreateTable(
                "dbo.UsersLastMoves",
                c => new
                    {
                        UsersLastMovesID = c.Int(nullable: false, identity: true),
                        UserId = c.String(maxLength: 128),
                        UsersLastMoveText = c.String(),
                        UsersMoveLink = c.String(),
                        MoveDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.UsersLastMovesID)
                .ForeignKey("dbo.User", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.IdentityRole",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.IdentityUserRole", "IdentityRole_Id", "dbo.IdentityRole");
            DropForeignKey("dbo.UsersLastMoves", "UserId", "dbo.User");
            DropForeignKey("dbo.UserRatings", "UserId", "dbo.User");
            DropForeignKey("dbo.Post", "UserId", "dbo.User");
            DropForeignKey("dbo.PostPhotos", "PostID", "dbo.Post");
            DropForeignKey("dbo.PostLikes", "PostID", "dbo.Post");
            DropForeignKey("dbo.PostComments", "PostID", "dbo.Post");
            DropForeignKey("dbo.UserPhotos", "UserId", "dbo.User");
            DropForeignKey("dbo.UserNotifications", "UserId", "dbo.User");
            DropForeignKey("dbo.UserHobbies", "UserId", "dbo.User");
            DropForeignKey("dbo.UserHobbies", "HobbiesID", "dbo.Hobbies");
            DropForeignKey("dbo.UserFollow", "UserId", "dbo.User");
            DropForeignKey("dbo.IdentityUserRole", "UserId", "dbo.User");
            DropForeignKey("dbo.IdentityUserLogin", "User_Id", "dbo.User");
            DropForeignKey("dbo.IdentityUserClaim", "UserId", "dbo.User");
            DropForeignKey("dbo.Chat", "UserId", "dbo.User");
            DropForeignKey("dbo.ChatMessages", "ChatId", "dbo.Chat");
            DropForeignKey("dbo.UserRatings", "CategoryItemID", "dbo.CategoryItems");
            DropForeignKey("dbo.CategoryItems", "CategoryID", "dbo.Categories");
            DropIndex("dbo.UsersLastMoves", new[] { "UserId" });
            DropIndex("dbo.PostPhotos", new[] { "PostID" });
            DropIndex("dbo.PostLikes", new[] { "PostID" });
            DropIndex("dbo.PostComments", new[] { "PostID" });
            DropIndex("dbo.Post", new[] { "UserId" });
            DropIndex("dbo.UserPhotos", new[] { "UserId" });
            DropIndex("dbo.UserNotifications", new[] { "UserId" });
            DropIndex("dbo.UserHobbies", new[] { "HobbiesID" });
            DropIndex("dbo.UserHobbies", new[] { "UserId" });
            DropIndex("dbo.UserFollow", new[] { "UserId" });
            DropIndex("dbo.IdentityUserRole", new[] { "IdentityRole_Id" });
            DropIndex("dbo.IdentityUserRole", new[] { "UserId" });
            DropIndex("dbo.IdentityUserLogin", new[] { "User_Id" });
            DropIndex("dbo.IdentityUserClaim", new[] { "UserId" });
            DropIndex("dbo.ChatMessages", new[] { "ChatId" });
            DropIndex("dbo.Chat", new[] { "UserId" });
            DropIndex("dbo.UserRatings", new[] { "CategoryItemID" });
            DropIndex("dbo.UserRatings", new[] { "UserId" });
            DropIndex("dbo.CategoryItems", new[] { "CategoryID" });
            DropTable("dbo.IdentityRole");
            DropTable("dbo.UsersLastMoves");
            DropTable("dbo.PostPhotos");
            DropTable("dbo.PostLikes");
            DropTable("dbo.PostComments");
            DropTable("dbo.Post");
            DropTable("dbo.UserPhotos");
            DropTable("dbo.UserNotifications");
            DropTable("dbo.Hobbies");
            DropTable("dbo.UserHobbies");
            DropTable("dbo.UserFollow");
            DropTable("dbo.IdentityUserRole");
            DropTable("dbo.IdentityUserLogin");
            DropTable("dbo.IdentityUserClaim");
            DropTable("dbo.ChatMessages");
            DropTable("dbo.Chat");
            DropTable("dbo.User");
            DropTable("dbo.UserRatings");
            DropTable("dbo.CategoryItems");
            DropTable("dbo.Categories");
        }
    }
}
