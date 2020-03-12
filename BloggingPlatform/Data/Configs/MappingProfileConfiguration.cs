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
            CreateMap<Post, PostToReturnDto>()
                .ForMember(fn => fn.AuthorFirstName, opt => opt.MapFrom(
                    src => src.Blog.Author.FirstName))
                .ForMember(ln => ln.AuthorLastName, opt => opt.MapFrom(
                    src => src.Blog.Author.LastName))
                .ForMember(l => l.Likes, opt => opt.MapFrom(
                    src => src.Likes.Count))
                .ForMember(n => n.BlogName, opt => opt.MapFrom(src => src.Blog.BlogName));
            CreateMap<Post, ArticleToReturnDto>()
                .ForMember(fn => fn.AuthorFirstName, opt => opt.MapFrom(
                    src => src.Blog.Author.FirstName))
                .ForMember(ln => ln.AuthorLastName, opt => opt.MapFrom(
                    src => src.Blog.Author.LastName))
                .ForMember(l => l.Likes, opt => opt.MapFrom(
                    src => src.Likes.Count));
            CreateMap<NewCommentDto, Comment>();
        }
    }
}
