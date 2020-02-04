using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloggingPlatform.Controllers
{
    [ApiController]
    [AllowAnonymous]
    [Route("api/[controller]")]
    public class HelloController : ControllerBase
    {
        private static readonly string[] values = new[]
        {
            "Hello", "from", "the", "first", "controller", "yay"
        };

        [HttpGet]
        public string[] Hello()
        {
            return values;
        }
    }
}