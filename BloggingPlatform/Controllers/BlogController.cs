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
    public class BlogController : ControllerBase
    {
        private readonly IBlogService service;
        private readonly IMapper mapper;
        public BlogController(IBlogService service, IMapper mapper)
        {
            this.service = service;
            this.mapper = mapper;
        }

        [AllowAnonymous]
        [HttpGet("{blogName}/post/{id}")]
        public async Task<IActionResult> GetPost(int id)
        {
            var post = await service.GetPost(id);

            if (post == null)
            {
                return NotFound();
            }

            var article = mapper.Map<ArticleToReturnDto>(post);

            return Ok(article);
        }

    }
}
