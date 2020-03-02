using BloggingPlatform.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloggingPlatform.Interfaces
{
    public interface IUsersService
    {
        void Add<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        Task<bool> SaveAll();
        Task<User> GetUser(int id);
        Task<int> GetPostOwnerId(int postId);
        Task<Like> GetLike(int postId, int likerId);
    }
}
