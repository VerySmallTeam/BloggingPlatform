using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloggingPlatform.Models
{
    public class Blog
    {
        public int Id { get; set; }
        public string BlogName { get; set; }
        public int AuthorId { get; set; }
        public User Author { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
    }
}
