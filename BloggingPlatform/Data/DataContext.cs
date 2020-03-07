using BloggingPlatform.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloggingPlatform.Data
{
    public class DataContext : IdentityDbContext<User, Role, int, IdentityUserClaim<int>,
        UserRole, IdentityUserLogin<int>, IdentityRoleClaim<int>, IdentityUserToken<int>>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<Comment> Comments { get; set; }
        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            #region UserRole
            builder.Entity<UserRole>(userRole =>
            {
                userRole.HasKey(ur => new { ur.UserId, ur.RoleId });

                userRole.HasOne(ur => ur.Role)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(ur => ur.RoleId)
                .IsRequired();

                userRole.HasOne(ur => ur.User)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(ur => ur.UserId)
                .IsRequired();
            });
            #endregion

            #region User
            builder.Entity<User>()
                .HasOne(b => b.Blog)
                .WithOne(u => u.Author)
                .HasForeignKey<Blog>(k => k.AuthorId);
            #endregion

            #region Blog
            builder.Entity<Blog>()
                .HasMany(p => p.Posts)
                .WithOne(b => b.Blog)
                .HasForeignKey(k => k.BlogId);
            #endregion

            #region Like
            builder.Entity<Like>()
               .HasKey(k => new { k.PostId, k.LikerId });

            builder.Entity<Like>()
                .HasOne(p => p.Post)
                .WithMany(l => l.Likes)
                .HasForeignKey(k => k.PostId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Like>()
                .HasOne(u => u.Liker)
                .WithMany(p => p.LikedPosts)
                .HasForeignKey(k => k.LikerId)
                .OnDelete(DeleteBehavior.Restrict);
            #endregion

            #region Comment
            builder.Entity<Comment>()
               .HasKey(k => new { k.PostId, k.CommenterId });

            builder.Entity<Comment>()
                .HasOne(p => p.Post)
                .WithMany(c => c.Comments)
                .HasForeignKey(k => k.PostId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Comment>()
                .HasOne(u => u.Commenter)
                .WithMany(c => c.Comments)
                .HasForeignKey(k => k.CommenterId)
                .OnDelete(DeleteBehavior.Restrict);
            #endregion
        }
    }
}