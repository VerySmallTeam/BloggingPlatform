using AutoMapper.Configuration;
using BloggingPlatform.Interfaces;
using BloggingPlatform.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloggingPlatform.Services
{
    public class AuthService : IAuthService
    {
        public Task<string> GenerateJwtToken(User user, UserManager<User> userManager, IConfiguration config)
        {
            throw new NotImplementedException();
        }
    }
}
