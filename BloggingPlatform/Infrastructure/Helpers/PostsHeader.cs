using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloggingPlatform.Infrastructure.Helpers
{
    public class PostsHeader
    {
        public int CurrentPart { get; set; }
        public int ItemsPerPart { get; set; }
        public PostsHeader(int currentPage, int itemsPerPage)
        {
            CurrentPart = currentPage;
            ItemsPerPart = itemsPerPage;
        }
    }
}
