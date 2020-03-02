using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloggingPlatform.Infrastructure.Helpers
{
    public class PostsList<T> : List<T>
    {
        public int CurrentPart { get; set; }
        public int PartSize { get; set; }
        public PostsList(List<T> items, int partNumber, int partSize)
        {
            CurrentPart = partNumber;
            PartSize = partSize;
            AddRange(items);
        }
        public static async Task<PostsList<T>> CreateAsync(IQueryable<T> posts, int partNumber, int partSize)
        {
            var items = await posts.Skip((partNumber - 1) * partSize).Take(partSize).ToListAsync();
            return new PostsList<T>(items, partNumber, partSize);
        }
    }
}
