using BloggingPlatform.Data;
using BloggingPlatform.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloggingPlatform.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdminController : ControllerBase
    {
        private readonly DataContext context;
        private readonly UserManager<User> userManager;

        public AdminController(DataContext context, UserManager<User> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }

        [Authorize(Policy = "RequireAdminRole")]
        [HttpGet("users-roles")]
        public async Task<IActionResult> GetUsersRoles()
        {
            var userList = await context.Users
                .OrderBy(u => u.UserName)
                .Select(user => new
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Roles = (from userRole in user.UserRoles
                             join role in context.Roles
                             on userRole.RoleId
                             equals role.Id
                             select role.Name).ToList()
                }).ToListAsync();

            return Ok(userList);
        }
    }
}
