using BloggingPlatform.Data;
using BloggingPlatform.Interfaces;
using BloggingPlatform.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloggingPlatform.Services
{
    public class UsersService : IUsersService
    {
        private readonly DataContext context;
        public UsersService(DataContext context)
        {
            this.context = context;
        }

        public void Add<T>(T entity) where T : class
        {
            context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            context.Remove(entity);
        }

        public void Update<T>(T entity) where T : class
        {
            context.Update(entity);
        }

        public async Task<bool> SaveAll()
        {
            return await context.SaveChangesAsync() > 0;
        }

        public async Task<User> GetUser(int id)
        {
            var user = await context.Users.FirstOrDefaultAsync(u => u.Id == id);
            return user;
        }

        public async Task<int> GetPostOwnerId(int postId)
        {
            var post = await context.Posts.FirstOrDefaultAsync(p => p.Id == postId);
            return post.Blog.AuthorId;
        }

        public async Task<Like> GetLike(int postId, int likerId)
        {
            var like = await context.Likes.FirstOrDefaultAsync(id => id.PostId == postId && id.LikerId == likerId);
            return like;
        }
    }
}
