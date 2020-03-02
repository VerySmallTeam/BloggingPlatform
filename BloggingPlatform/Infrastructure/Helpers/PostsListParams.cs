using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloggingPlatform.Infrastructure.Helpers
{
    public class PostsListParams
    {
        private const int size = 10; 
        public int PartSize
        {
            get { return size; }
        }
        public int PartNumber { get; set; } = 1;
    }
}
