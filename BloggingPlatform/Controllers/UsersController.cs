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

        [HttpPost("{userId}/posts/create-new-post")]
        public async Task<IActionResult> AddPost(int userId, NewPostDto newPostDto)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
            {
                return Unauthorized();
            }

            var user = await usersService.GetUser(userId);

            newPostDto.BlogId = user.Blog.Id;
            var post = mapper.Map<Post>(newPostDto);

            usersService.Add<Post>(post);

            if (await usersService.SaveAll())
            {
                return Ok();
            }

            return BadRequest("Failed to add post");
        }

        [HttpDelete("{userId}/posts/{postId}")]
        public async Task<IActionResult> DeletePost(int userId, int postId)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
            {
                return Unauthorized();
            }

            var user = await usersService.GetUser(userId);

            if (user.Blog.Posts.FirstOrDefault(p => p.Id == postId).Id != postId)
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

        [HttpPost("{userId}/posts/like/{postId}")]
        public async Task<IActionResult> LikePost(int postId, int userId)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
            {
                return Unauthorized();
            }

            var like = await usersService.GetLike(postId, userId);

            if (like != null)
            {
                return BadRequest("You already like this post");
            }

            if (await blogService.GetPost(postId) == null)
            {
                return NotFound();
            }

            if (await usersService.GetPostOwnerId(postId) == userId)
            {
                return BadRequest("You can not like your own post");
            }

            like = new Like
            {
                PostId = postId,
                LikerId = userId
            };

            usersService.Add<Like>(like);

            if (await usersService.SaveAll())
            {
                return Ok();
            }

            return BadRequest("Failed to like post");
        }

        [HttpDelete("{userId}/posts/like/{postId}")]
        public async Task<IActionResult> UnlikePost(int userId, int postId)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
            {
                return Unauthorized();
            }

            var like = await usersService.GetLike(postId, userId);

            if (like == null)
            {
                return NotFound();
            }

            usersService.Delete<Like>(like);

            if (await usersService.SaveAll())
            {
                return Ok();
            }

            return BadRequest("Failed to unlike post");
        }
    }
}
