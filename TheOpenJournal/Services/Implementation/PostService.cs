using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TheOpenJournal.Mapper;
using TheOpenJournal.Models.Domain;
using TheOpenJournal.Models.DTOs;
using TheOpenJournal.Repository.Interfaces;
using TheOpenJournal.Services.Interfaces;

namespace TheOpenJournal.Services.Implementation
{
    public class PostService:IPostService
    {
        private readonly IPostRepository _postRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IUtilityService _utilityService;
        public PostService(IPostRepository postRepository, IMapper mapper,
            ICategoryRepository categoryRepository,
            IUserRepository userRepository, IUtilityService utilityService){ 
            _postRepository = postRepository;
            _mapper = mapper;
            _categoryRepository = categoryRepository;
            _userRepository = userRepository;
            _utilityService = utilityService;
        }

        //Service for adding posts.
        public async Task<bool> AddPostAsync(PostDTO postDto)
        {
            
            var post =  _mapper.Map<Post>(postDto);
            if (postDto.FeaturedImage != null)
            {
                string url = await _utilityService.SaveImageAsync(postDto.FeaturedImage);
                post.FeaturedImageUrl = url ?? post.FeaturedImageUrl;
            }
            //map categories
            var categories = _categoryRepository.GetQueryable()
                .Where(c => postDto.CategoryId.Contains(c.Id))
                .ToList();

            post.Categories = categories;
            //get user and map him.
            var user = await _userRepository.GetEmailAsync(postDto.User);
            post.UserId = user.Id;
            post.User = user;

           int response = await _postRepository.AddAsync(post);
            if (response <= 0) {
                return false;
            }
            else
            {
                return true;
            }
        }
        //Method for Getting all posts.
        public async Task<List<GetPostDTO>> GetPostsAsync()
        {
            try
            {
                //get all posts from database
                var posts = await _postRepository.GetQueryable()
                    .Include(category=>category.Categories)
                    .Include(user=>user.User)
                    .ToListAsync();

                //map the posts into GetPostDto
                var allPosts = _mapper.Map<List<GetPostDTO>>(posts);
                // return posts to controller.


                return allPosts;
                

            }catch(Exception ex)
            {
                throw new Exception("Exception While Getting Post: " + ex);
            }
        }
    }
}
