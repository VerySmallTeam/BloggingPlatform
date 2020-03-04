using BloggingPlatform.Infrastructure.Helpers;
using BloggingPlatform.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloggingPlatform.Interfaces
{
    public interface IBlogService
    {
        Task<PostsList<Post>> GetPostsList(PostsListParams postsListParams);
        Task<Post> GetPost(int id);
        Task<List<Post>> GetTopPosts();
    }
}
