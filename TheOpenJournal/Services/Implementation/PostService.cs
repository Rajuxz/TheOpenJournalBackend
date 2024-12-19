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
                else
                {
                    post.FeaturedImageUrl = null;
                }
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
        public async Task<List<PostDTO>> GetPostsAsync()
        {
            try
            {
                //get all posts from database
                var posts = _postRepository.GetQueryable();

                //map the posts into PostDto
                // return posts to controller.


                var data = new List<PostDTO>
                {
                     new PostDTO
                     {
                        Title = "Learn Async/Await in C#",
                        Content = "This post explains how to use async/await in C# for better performance.",
                        Slug = "learn-async-await-csharp",
                        FeaturedImage = null, // Normally this would be a file uploaded by the user
                        CategoryId = new List<Guid> { Guid.NewGuid(), Guid.NewGuid() },
                        User = "John Doe"
                     },
                    new PostDTO
                     {
                        Title = "Learn Async/Await in C++",
                        Content = "This post explains how to use async/await in C++ for better performance.",
                        Slug = "learn-async-await-csharp",
                        FeaturedImage = null, // Normally this would be a file uploaded by the user
                        CategoryId = new List<Guid> { Guid.NewGuid(), Guid.NewGuid() },
                        User = "John Doe"
                     },
                };
                return data;

            }catch(Exception ex)
            {
                throw new Exception("Exception While Getting Post: " + ex);
            }
        }
    }
}
