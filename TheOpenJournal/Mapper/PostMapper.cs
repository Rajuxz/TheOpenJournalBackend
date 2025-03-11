using AutoMapper;
using TheOpenJournal.Models.Domain;
using TheOpenJournal.Models.DTOs;

namespace TheOpenJournal.Mapper
{
    public class PostMapper : Profile
    {
        public PostMapper()
        {
            //PostDto to Post
            CreateMap<PostDTO, Post>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.FeaturedImageUrl, opt => opt.Ignore())
                .ForMember(dest => dest.Categories, opt => opt.Ignore())
                .ForMember(dest => dest.User, opt => opt.Ignore())
                .ForMember(dest => dest.UserId, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                ;

            //Post to GetPostDto

          CreateMap<Post, GetPostDTO>()
            .ForMember(dest => dest.Categories, opt => opt.MapFrom(src => src.Categories))
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId));


            CreateMap<Category, CategoryDTO>();

            //Map non-null fields only.
            CreateMap<UpdatePostDTO, Post>()
             .ForMember(dest=>dest.User,opt=>opt.Ignore())
             .ForMember(dest=>dest.UserId,opt=>opt.Ignore())
             .ForMember(dest=>dest.UpdatedAt, opt=>opt.MapFrom(src=>DateTime.UtcNow))
             .ForMember(dest => dest.Categories, opts =>
                    opts.Condition(src => src.Categories != null && src.Categories.Any()))
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }

        private ICollection<Category> MapCategories(ICollection<UpdatePostCategoryDto> source, ICollection<Category> target)
        {
            target.Clear(); //avoid duplicate by clearing the existing values.
            foreach (var value in source)
            {

                var category = new Category()
                {
                    Id = value.Id//avoid null value                  
                };
                target.Add(category);

            }
            return target;
        }
    }
}
