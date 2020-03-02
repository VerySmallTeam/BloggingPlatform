using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloggingPlatform.DTO
{
    public class NewPostDto
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime DateAdded { get; set; }
        public int BlogId { get; set; }
        public NewPostDto()
        {
            DateAdded = DateTime.Now;
        }
    }
}
