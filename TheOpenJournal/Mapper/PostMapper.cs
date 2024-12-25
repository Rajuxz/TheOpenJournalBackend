﻿using AutoMapper;
using TheOpenJournal.Models.Domain;
using TheOpenJournal.Models.DTOs;

namespace TheOpenJournal.Mapper
{
    public class PostMapper:Profile
    {
        public PostMapper() {
            //PostDto to Post
            CreateMap<PostDTO, Post>()
                .ForMember(dest=>dest.Id,opt=>opt.Ignore())
                .ForMember(dest => dest.FeaturedImageUrl, opt => opt.Ignore())
                .ForMember(dest => dest.Categories, opt => opt.Ignore())
                .ForMember(dest => dest.UniqueViewCount, opt => opt.Ignore())
                .ForMember(dest => dest.LikeCount, opt => opt.Ignore())
                .ForMember(dest => dest.CommentCount, opt => opt.Ignore())
                .ForMember(dest=>dest.User,opt=>opt.Ignore())
                .ForMember(dest => dest.UserId, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt,opt=>opt.Ignore())
                .ForMember(dest =>  dest.UpdatedAt,opt=>opt.Ignore())
                ;

            //Post to GetPostDto

            CreateMap<Post, GetPostDTO>()
                .ForMember(dest=>dest.Categories,opt=>opt.MapFrom(src=>src.Categories))
                .ForMember(dest=>dest.UserId,src=>src.MapFrom(src=>src.UserId));

            CreateMap<Category, CategoryDTO>();

            //Map non-null fields only.
            CreateMap<UpdatePostDTO, Post>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}
