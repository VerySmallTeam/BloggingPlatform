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
        void Add<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        Task<bool> SaveAll();
        Task<PostsList<Post>> GetPostsList(PostsListParams postsListParams);
        Task<Post> GetPost(int id);
    }
}
