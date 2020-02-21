using BloggingPlatform.Infrastructure.Helpers;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloggingPlatform.Infrastructure.Extensions
{
    public static class AddPostsHeaderExtension
    {
        public static void AddPostHeader(
            this HttpResponse response, int currentPage, int itemsPerPage)
        {
            var paginationHeader = new PostsHeader(currentPage, itemsPerPage);
            var camelCaseFormater = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
            response.Headers.Add("Pagination", JsonConvert.SerializeObject(paginationHeader, camelCaseFormater));
            response.Headers.Add("Access-Control-Expose-Headers", "Posts");

        }
    }
}
