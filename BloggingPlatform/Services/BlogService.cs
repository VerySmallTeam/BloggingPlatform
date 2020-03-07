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

        public async Task<List<Post>> GetTopPosts()
        {
            var posts = await context.Posts
               .OrderByDescending(p => p.Likes.Count())
               .Take(5)
               .ToListAsync();

            return posts;
        }

        public async Task<Comment> GetComment(int postId, int commenterId)
        {
            var comment = await context.Comments
                .FirstOrDefaultAsync(id => id.PostId == postId && id.CommenterId == commenterId);

            return comment;
        }

        public async Task<Comment> GetCommentById(int postId, int commenterId, int commentId)
        {
            var comment = await context.Comments
                .FirstOrDefaultAsync(id => id.PostId == postId && id.CommenterId == commenterId && id.Id == commentId);

            return comment;
        }

    }
}
