using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloggingPlatform.DTO
{
    public class NewCommentDto
    {
        public int PostId{ get; set; }
        public int CommenterId { get; set; }
        public string Content { get; set; }
    }
}
