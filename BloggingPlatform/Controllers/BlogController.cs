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
    [Route("api/users/{userId}/[controller]")]
    [ApiController]
    [Authorize]
    public class BlogController : ControllerBase
    {
        private readonly IBlogService service;
        private readonly IMapper mapper;
        private readonly IUsersService usersService;
        public BlogController(IBlogService service, IMapper mapper, IUsersService usersService)
        {
            this.service = service;
            this.mapper = mapper;
            this.usersService = usersService;
        }

        [HttpPost("{blogId}/post")]
        public async Task<IActionResult> AddPost(int userId, int blogId, NewPostDto newPostDto)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
            {
                return Unauthorized();
            }

            var user = await usersService.GetUser(userId);

            if (user.Blog.Id != blogId)
            {
                return Unauthorized();
            }

            newPostDto.BlogId = blogId;
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
