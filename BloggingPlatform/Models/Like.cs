using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloggingPlatform.Models
{
    public class Like
    {
        public int PostId { get; set; }
        public virtual Post Post { get; set; }
        public int LikerId { get; set; }
        public virtual User Liker { get; set; }
    }
}
