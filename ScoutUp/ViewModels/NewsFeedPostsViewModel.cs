using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ScoutUp.ViewModels
{
    public class NewsFeedPostsViewModel
    {
        public int PostId { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string UserSurname { get; set; }
        public string CurrentUserPhoto { get; set; }
        public string PostOwnerPhoto { get; set; }
        public DateTime PostDatePosted { get; set; }
        public string PostText { get; set; }
        public PostPhotoViewModel PostPhotos { get; set; }
    }

    public class PostPhotoViewModel
    {
        public List<PostPhotosModel> PostPhotos { get; set; }
    }
    public class PostPhotosModel
    {
        public string PostPhotoLocateBig { get; set; }
    }

    public class PostCommentViewModel
    {
        public int PostId { get; set; }
        public int CommentCount { get; set; }
        public int Count { get; set; }
        public int RemainingCommentCount { get; set; }

        public List<PostCommentModel> PostCommentModel { get; set; }


    }
    public class PostCommentModel
    {
        public string PostComment { get; set; }
        public DateTime PostCommentDate { get; set; }
        public string UserName { get; set; }
        public string UserSurname { get; set; }
        public string UserProfilePhoto { get; set; }
        public int UserId { get; set; }
    }
}