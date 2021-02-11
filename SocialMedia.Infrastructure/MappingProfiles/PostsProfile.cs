using AutoMapper;
using SocialMedia.Core.Dtos;
using SocialMedia.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SocialMedia.Infrastructure.MappingProfiles
{
    public class PostsProfile : Profile
    {
        public PostsProfile()
        {
            CreateMap<Post, PostDto>();
            CreateMap<PostDto, Post>();
        }
    }
}
