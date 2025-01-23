using AutoMapper;
using Microsoft.AspNetCore.Mvc.Razor.TagHelpers;
using Microsoft.EntityFrameworkCore;
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
        private readonly IHttpContextAccessor _httpContextAccessor;
        public PostService(IPostRepository postRepository, IMapper mapper,
            ICategoryRepository categoryRepository,
            IHttpContextAccessor httpContextAccessor,
            IUserRepository userRepository, IUtilityService utilityService){ 
            _postRepository = postRepository;
            _mapper = mapper;
            _categoryRepository = categoryRepository;
            _userRepository = userRepository;
            _utilityService = utilityService;
            _httpContextAccessor = httpContextAccessor;
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
        //GetPostsByCategoryAsync
        public async Task<List<GetPostDTO>> GetPostsByCategoryAsync(Guid guid)
        {
            try
            {
                var posts = await _categoryRepository.GetQueryable()
                    .Where(category => category.Id == guid)
                    .SelectMany(category => category.Posts)
                    .ToListAsync();

                var getPostDto = _mapper.Map<List<GetPostDTO>>(posts);
                return getPostDto;
            }catch(Exception ex)
            {
                throw new Exception("Exception while getting Posts !" + ex);
            }
        }

        // Update Posts
       public async Task<bool> UpdatePostAsync(UpdatePostDTO updatePostDto)
       {
            try
            {
                //check if post id is correct.
                var post = await _postRepository.GetByIdAsync(updatePostDto.Id);
                if (post != null)
                {
                    //if post exists, update updated fields.
                    _mapper.Map(updatePostDto, post);

                    if(updatePostDto.Categories != null)
                    {
                        post.Categories.Clear();
                        foreach(var categories in updatePostDto.Categories)
                        {
                            post.Categories.Add(new Category { Id = categories.Id });
                        }
                    }

                    int response = await _postRepository.UpdateAsync(post);
                    if(response <= 0)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                else
                {
                    return false;
                }
            }catch(Exception ex)
            {
                throw new Exception("Exception while updating post !"+ex.Message);
            }
       }
        //Get post posted by specific user.
        public async Task<List<GetPostDTO>> GetPostByUserIdAsync(string email)
        {
            try
            {
                if (email != null)
                {
                    var userId = await _userRepository.GetIdByEmailAsync(email);
                    var posts =await _postRepository.GetQueryable()
                        .Where(post => post.UserId == userId)
                        .ToListAsync();
                    //map to GetPostDto
                    if(posts.Count > 0)
                    {
                        var postDto = _mapper.Map<List<GetPostDTO>>(posts);
                        return postDto;
                    }
                    else
                    {
                        return new List<GetPostDTO>();
                    }
                }
                else
                {
                    throw new Exception("Invalid email provided");
                }
            }catch(Exception ex)
            {
                throw new Exception("Exception in GetPostByUserIdAsync Method" + ex.Message);
            }
        }
    }
}
