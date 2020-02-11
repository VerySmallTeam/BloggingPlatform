using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloggingPlatform.DTO
{
    public class UserForListDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastActive { get; set; }
        public string Avatar { get; set; }
    }
}
