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
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IUtilityService _utilityService;
        public PostService(IPostRepository repository,IMapper mapper,
            ICategoryRepository categoryRepository,
            IUserRepository userRepository, IUtilityService utilityService){ 
            _repository = repository;
            _mapper = mapper;
            _categoryRepository = categoryRepository;
            _userRepository = userRepository;
            _utilityService = utilityService;
        }

        public async Task<Post> AddPostAsync(PostDTO postDto)
        {
            
            var post =  _mapper.Map<Post>(postDto);
            if (postDto.FeaturedImage == null)
            {
                post.FeaturedImageUrl = null;
            }
            else
            {
                string url = await _utilityService.SaveImage(postDto.FeaturedImage);
                if (url != null)
                {
                    post.FeaturedImageUrl = url;
                }
                post.FeaturedImageUrl = null;
            }
            //map categories
            var categories = _categoryRepository.GetQueryable()
                .Where(c => postDto.CategoryId.Contains(c.Id))
                .ToList();

            post.Categories = categories;
            //get user and map him.
            var user = await _userRepository.GetEmailAsync(postDto.User);
            post.User = user;

            return post;
        }
    }
}
