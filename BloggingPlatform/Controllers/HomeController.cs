using AutoMapper;
using BloggingPlatform.DTO;
using BloggingPlatform.Infrastructure.Helpers;
using BloggingPlatform.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BloggingPlatform.Infrastructure.Extensions;

namespace BloggingPlatform.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class HomeController : ControllerBase
    {
        private readonly IBlogService service;
        private readonly IMapper mapper;
        public HomeController(IBlogService service, IMapper mapper)
        {
            this.service = service;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetPosts([FromQuery]PostsListParams postsListParams)
        {
            var posts = await service.GetPostsList(postsListParams);
            var postToReturn = mapper.Map<IEnumerable<PostToReturnDto>>(posts);
            Response.AddPostHeader(posts.CurrentPart, posts.PartSize);
            return Ok(postToReturn);
        }

        [AllowAnonymous]
        [HttpGet("get-top-posts")]
        public async Task<IActionResult> GetTopPosts()
        {
            var posts = await service.GetTopPosts();
            var postToReturn = mapper.Map<IEnumerable<PostToReturnDto>>(posts);
            return Ok(postToReturn);
        }
    }
}
