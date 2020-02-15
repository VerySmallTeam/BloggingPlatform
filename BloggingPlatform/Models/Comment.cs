using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloggingPlatform.Models
{
    public class Comment
    {
        public int PostId { get; set; }
        public virtual Post Post { get; set; }
        public int CommenterId { get; set; }
        public virtual User Commenter { get; set; }
        public string Content { get; set; }
    }
}
