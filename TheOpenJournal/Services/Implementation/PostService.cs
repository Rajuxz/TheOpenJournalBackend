using AutoMapper;
using TheOpenJournal.Mapper;
using TheOpenJournal.Models.Domain;
using TheOpenJournal.Models.DTOs;
using TheOpenJournal.Repository.Interfaces;
using TheOpenJournal.Services.Interfaces;

namespace TheOpenJournal.Services.Implementation
{
    public class PostService:IPostService
    {
        private readonly IPostRepository _repository;
        private readonly IMapper _mapper;
        public PostService(IPostRepository repository,IMapper mapper){ 
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Post> AddPostAsync(PostDTO postDto)
        {
            
            var post =  _mapper.Map<Post>(postDto);
            //map categories
            post.Categories = postDto.CategoryId.Select(id=>new Category {Id = id}).ToList();
            //user
            return post;
        }
    }
}
