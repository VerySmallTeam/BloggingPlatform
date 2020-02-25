using BloggingPlatform.Data;
using BloggingPlatform.Infrastructure.Helpers;
using BloggingPlatform.Interfaces;
using BloggingPlatform.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloggingPlatform.Services
{
    public class BlogService : IBlogService
    {
        private readonly DataContext context;
        public BlogService(DataContext context)
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

        public async Task<bool> SaveAll()
        {
            return await context.SaveChangesAsync() > 0;
        }
        public async Task<PostsList<Post>> GetPostsList(PostsListParams postsListParams)
        {
            var posts = context.Posts
                .OrderByDescending(o => o.DateAdded)
                .AsQueryable();

            return await PostsList<Post>.CreateAsync(posts, postsListParams.PartNumber, postsListParams.PartSize);
        }

        public async Task<Post> GetPost(int postId)
        {
            var post = await context.Posts.FirstOrDefaultAsync(p => p.Id == postId);
            return post;
        }

    }
}
