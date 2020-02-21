using AutoMapper;
using BloggingPlatform.DTO;
using BloggingPlatform.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloggingPlatform.Data.Configs
{
    public class MappingProfileConfiguration : Profile
    {
        public MappingProfileConfiguration()
        {
            CreateMap<User, UserForListDto>();
            CreateMap<UserForRegisterDto, User>();
            CreateMap<NewPostDto, Post>();
            CreateMap<Post, PostToReturnDto>();
        }
    }
}
