using AutoMapper.Configuration;
using BloggingPlatform.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloggingPlatform.Interfaces
{
    public interface IAuthService
    {
        Task<string> GenerateJwtToken(User user, UserManager<User> userManager, IConfiguration config);
    }
}
