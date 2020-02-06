using AutoMapper;
using BloggingPlatform.DTO;
using BloggingPlatform.Interfaces;
using BloggingPlatform.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BloggingPlatform.Infrastructure.Helpers;

namespace BloggingPlatform.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration config;
        private readonly IMapper mapper;
        private readonly IAuthService service;
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;

        public AuthController(IConfiguration config, IMapper mapper, IAuthService service,
            UserManager<User> userManager, SignInManager<User> signInManager)
        {
            this.config = config;
            this.mapper = mapper;
            this.service = service;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginDto userLoginDto)
        {
            if (!RequestValidation.IsRequestValid<UserLoginDto>(userLoginDto))
            {
                return BadRequest("Invalid request");
            }

            var user = await userManager.FindByEmailAsync(userLoginDto.Email);

            if (user == null)
            {
                return Unauthorized();
            }

            var result = await signInManager.CheckPasswordSignInAsync(user, userLoginDto.Password, false);

            if (result.Succeeded)
            {
                var appUser = mapper.Map<UserForListDto>(user);
                return Ok(new
                {
                    token = service.GenerateJwtToken(user, userManager, config).Result,
                    user = appUser
                });
            }
            return Unauthorized();
        }
    }
}
