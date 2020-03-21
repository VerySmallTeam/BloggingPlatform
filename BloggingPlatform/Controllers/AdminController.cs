using BloggingPlatform.Data;
using BloggingPlatform.DTO;
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

        [Authorize(Policy = "RequireAdminRole")]
        [HttpPost("editRoles/{userName}")]
        public async Task<IActionResult> EditRoles(string userName, EditRolesDto editRolesDto)
        {
            var user = await userManager.FindByNameAsync(userName);
            var userRoles = await userManager.GetRolesAsync(user);
            var selectedRoles = editRolesDto.RoleNames;

            selectedRoles = selectedRoles ?? new string[] { };

            var result = await userManager.AddToRolesAsync(user, selectedRoles.Except(userRoles));

            if (!result.Succeeded)
            {
                return BadRequest("Failes to add to roles");
            }

            result = await userManager.RemoveFromRolesAsync(user, userRoles.Except(selectedRoles));

            if (!result.Succeeded)
            {
                return BadRequest("Failed to remove the roles");
            }

            return Ok(await userManager.GetRolesAsync(user));
        }
    }
}
