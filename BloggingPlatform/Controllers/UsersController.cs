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
        private readonly IMapper mapper;
        private readonly IUsersService service;
        public UsersController(IMapper mapper, IUsersService service)
        {
            this.service = service;
            this.mapper = mapper;
        }

        [HttpPost("{userId}/{blogName}/create-new-post")]
        public async Task<IActionResult> AddPost(int userId, string blogName, NewPostDto newPostDto)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
            {
                return Unauthorized();
            }

            var user = await service.GetUser(userId);

            if (!user.Blog.BlogName.Equals(blogName))
            {
                return Unauthorized();
            }

            newPostDto.BlogId = user.Blog.Id;
            var post = mapper.Map<Post>(newPostDto);

            service.Add<Post>(post);

            if (await service.SaveAll())
            {
                return Ok();
            }

            return BadRequest("Failed to add post");
        }
    }
}
