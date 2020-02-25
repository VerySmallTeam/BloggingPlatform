using AutoMapper;
using BloggingPlatform.DTO;
using BloggingPlatform.Interfaces;
using BloggingPlatform.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BloggingPlatform.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService usersService;
        private readonly IBlogService blogService;
        private readonly IMapper mapper;

        public UsersController(IMapper mapper, IUsersService usersService, IBlogService blogService)
        {
            this.usersService = usersService;
            this.blogService = blogService;
            this.mapper = mapper;
        }

        [HttpPost("{userId}/{blogName}/create-new-post")]
        public async Task<IActionResult> AddPost(int userId, string blogName, NewPostDto newPostDto)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
            {
                return Unauthorized();
            }

            var user = await usersService.GetUser(userId);

            if (!user.Blog.BlogName.Equals(blogName))
            {
                return Unauthorized();
            }

            newPostDto.BlogId = user.Blog.Id;
            var post = mapper.Map<Post>(newPostDto);

            usersService.Add<Post>(post);

            if (await usersService.SaveAll())
            {
                return Ok();
            }

            return BadRequest("Failed to add post");
        }

        [HttpDelete("{userId}/{blogName}/post/{postId}")]
        public async Task<IActionResult> DeletePost(int userId, int postId)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
            {
                return Unauthorized();
            }

            var postToDelete = await blogService.GetPost(postId);
            usersService.Delete<Post>(postToDelete);

            if (await usersService.SaveAll())
            {
                return Ok();
            }

            return BadRequest("Failed to delete post");
        }
    }
}
