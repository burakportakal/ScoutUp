using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using ScoutUp.Models;
namespace ScoutUp.DAL
{
    public class ScoutUpDB : DbContext
    {
        public ScoutUpDB() : base("ScoutUpDB") { }
        public DbSet<User> Users {get;set;}
    public DbSet<Post> Posts {get;set;}
    public DbSet<Hobbies> Hobbies {get;set;}
    public DbSet<PostComments> PostComments {get;set;}
    public DbSet<PostLikes> PostLikes {get;set;}
    public DbSet<PostPhotos> PostPhotos {get;set;}
    public DbSet<PostPhotosLocation> PostPhotosLocation { get;set;}
    public DbSet<UserFollow> UserFollow {get;set;}
    public DbSet<UserHobbies> UserHobbies {get;set;}
    public DbSet<UserPhotos> UserPhotos {get;set;}
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}