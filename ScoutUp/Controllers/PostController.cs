using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using ScoutUp.Classes;
using ScoutUp.DAL;
using ScoutUp.Models;
using ScoutUp.Repository;

namespace ScoutUp.Controllers
{
    [Authorize]
    public class PostController : Controller
    {
        private readonly ScoutUpDB _db = new ScoutUpDB();
        // GET: Post
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Posts(int? userid)
        {
            var currentUser = GetUserModel();
            var user =userid==null ? currentUser : _db.UserById((int)userid);
            if(user==null) return HttpNotFound();
            user.UserPosts=  user.UserPosts.OrderByDescending(e => e.PostDatePosted).ToList();
            ViewBag.currentUser = currentUser;
            var posts = user.UserPosts;
            return PartialView("userPosts",user);
        }

        public ActionResult PostComments(int? id,int count=5)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Post post;
                post = _db.PostById((int) id);

            var comments = new List<PostComments>();
                var users = new List<User>();
                var counter = 1;
                var commentCount = post.PostComments.Count;
                foreach (var comment in post.PostComments.Reverse())
                {
                    comments.Add(comment);
                        users.Add(_db.UserById(comment.UserID));
                   counter++;
                    if (counter == count) break;
                }
                ViewBag.comments = comments;
                ViewBag.users = users;
                ViewBag.postid = id;
                ViewBag.commentCount = commentCount;
                ViewBag.remainingComment = commentCount - count;
                ViewBag.count = count;
            return PartialView("postComments");
        }
        [HttpPost]
        public ActionResult AddCommentToPost(int? postid,string comment)
        {
            if (postid == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var post = _db.PostById((int)postid);
            if(post ==null ) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var user = GetUserModel();
            var userComment = new PostComments { UserID = user.UserID,PostID = (int)postid,PostComment = comment,PostCommentDate = DateTime.Now};
            
            _db.PostComments.Add(userComment);
            try
            {
                _db.SaveChanges();
                return Json(1);
            }
            catch
            {
                return Json(0);
            }

        }
        [HttpPost]
        public ActionResult AddPost(string postText)
        {
            if(String.IsNullOrEmpty(postText)) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var user = GetUserModel();
            var post = new Post
            {
                UserID=user.UserID,
                PostText = postText,
                PostDatePosted = DateTime.Now
               
            };
            try
            {
                //yeni post kaydedilip id'si alınıyor
                using (var context = new ScoutUpDB())
                {
                    context.Posts.Add(post);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, error = true, message = ex.InnerException.ToString() });
            }
            var photo = new PostPhotos();
            var postPhotoLocationList = new List<PostPhotosLocation>();
            var resizer = new ImageResize();
            if (Request.Files.Count > 0)
            {
                photo.PostID = post.PostID;
                HttpFileCollectionBase files = Request.Files;

                try
                {
                    //varsa resim kaydedilip id si alınıyor
                    using (var context = new ScoutUpDB())
                    {
                        context.PostPhotos.Add(photo);
                        context.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    _db.Posts.Remove(post);
                    _db.SaveChanges();
                    return Json(new { success = false, error = true, message = ex.InnerException.ToString() });
                }
                for (int i=0;i< files.Count;i++)
                {
                    if (files[i] != null && files[i].ContentLength > 0)
                    {
                        Image imgBig = resizer.RezizeImage(Image.FromStream(files[i].InputStream, true, true), 640, 640);
                        var fileName = Path.GetFileName(files[i].FileName);
                        var fileNameBig = fileName.Replace(".", "-big.");
                        var pathBig = Path.Combine(Server.MapPath("~/images/post-images"), fileNameBig);
                        var databasePathBig = "../../images/post-images/" + fileNameBig;
                        imgBig.Save(pathBig);
                        //bir gönderiye birden fazla resim eklenebiliniyor.
                        var postPhotoLocation = new PostPhotosLocation
                        {
                            PostPhotosID = photo.PostPhotosID,
                            PostPhotosLocateBig = databasePathBig
                        };
                        postPhotoLocationList.Add(postPhotoLocation);
                    }
                }
            }

            try
            {
                using (var context = new ScoutUpDB())
                {
                    //gönderinin resimlerinin sunucuda ki yerleri kaydediliyor
                    context.PostPhotosLocation.AddRange(postPhotoLocationList);
                    context.SaveChanges();
                    return Json(new { success = true });
                }
            }
            catch (Exception ex)
            {
                _db.Posts.Remove(post);
                _db.PostPhotos.Remove(photo);
                _db.SaveChanges();
                return Json(new { success = false,error =true,message =ex.InnerException.ToString() });
            }

        }

        public ActionResult LikePost(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var post = _db.PostById((int)id);
            
            var user = GetUserModel();
            var isLiked = _db.PostLikes.Where(e => e.UserID == user.UserID).FirstOrDefault(e => e.PostID == id);
            if (isLiked != null)
            {
                    _db.PostLikes.Attach(isLiked);
                    _db.Entry(isLiked).State = EntityState.Deleted;
                    _db.SaveChanges();
                    //NotificationRepository repo=new NotificationRepository();
                    //repo.AddNotification(post.UserID,user.UserName+ " " + user.UserSurname +" gönderini beğendi");
                    return Json(new { success = true,liked = false, likes = _db.PostById((int)id).PostLikes.Count }, JsonRequestBehavior.AllowGet);
                }
            var postLike = new PostLikes
            {
                UserID = user.UserID,
                PostID = (int) id,
                IsLiked = true
            };

            try
            {
                using (var context = new ScoutUpDB())
                {
                    context.PostLikes.Add(postLike);
                    context.SaveChanges();
                    return Json(new { success = true,liked=true,likes =context.PostById((int)id).PostLikes.Count },JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {

                return Json(new { success = false, error = true, message = ex.InnerException.ToString() },JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult UserLikedPost(int? id)
        {
            if ( id ==null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var user = GetUserModel();
            PostLikes isLiked;

            isLiked = _db.PostLikes.Where(e => e.UserID == user.UserID).FirstOrDefault(e => e.PostID == id);

            if (isLiked == null)
            {
                ViewBag.isLiked = false;
                ViewBag.postid = id;
                ViewBag.postUserid = _db.Posts.FirstOrDefault(e => e.PostID == (int) id).UserID;
                ViewBag.postLikesCount = _db.PostById((int)id).PostLikes.Count;
                return PartialView("UserLikedPost");
            }
            else
            {
                ViewBag.isLiked = true;
                ViewBag.postid = id;
                ViewBag.postUserid = _db.Posts.FirstOrDefault(e => e.PostID == (int)id).UserID;
                ViewBag.postLikesCount = _db.PostById((int)id).PostLikes.Count;
                return PartialView("UserLikedPost");
            }
        }

        public ActionResult NewsFeedPosts(int count=0)
        {
            var user = GetUserModel();
            List<UserFollow> usersFollowing = user.UserFollow.ToList();
            List<Post> posts= new List<Post>();
            List<Post> userPost=new List<Post>();
                foreach (var following in usersFollowing)
                {
                    // use extencions 
                    //https://stackoverflow.com/questions/3356541/entity-framework-linq-query-include-multiple-children-entities

                    var temp = _db.CompletePosts().Where(u => u.UserID == following.UserBeingFollowedUserID);
                    var x = _db.Posts.SqlQuery("select * from post where UserID ="+following.UserBeingFollowedUserID).ToArray();
                    
                    posts.AddRange(temp.OrderByDescending(e => e.PostDatePosted).Skip(0 + count).Take(5 + count).ToList());
                }
                userPost=_db.CompletePosts().Where(u => u.UserID == user.UserID).ToList();

            //List<Post> posts = new List<Post>();
            //foreach (var u in users)
            //{

            //    try
            //    {
            //        var userPosts = u.UserPosts.OrderByDescending(e => e.PostDatePosted).Skip(0 + count).Take(5 + count).ToList();
            //        foreach (var post in userPosts)
            //        {
            //            posts.Add(post);
            //        }
            //    }
            //    catch (ArgumentNullException)
            //    {
            //        continue;
            //    }
                
            //}
            try
            {
                posts.AddRange(userPost.OrderByDescending(e => e.PostDatePosted).Skip(0 + count).Take(5 + count).ToList());
                posts = posts.OrderByDescending(e => e.PostDatePosted).ToList();
            }
            catch
            {
                // ignored
            }

            ViewBag.posts = posts;
            return PartialView("NewsFeedPosts",user);
        }
        
        private User GetUserModel()
        {
            return GetUserController().GetUser();
        }
        private UsersController GetUserController()
        {
            var controller = DependencyResolver.Current.GetService<UsersController>();
            controller.ControllerContext = new ControllerContext(this.Request.RequestContext, controller);
            return controller;
        }
    }

   
}