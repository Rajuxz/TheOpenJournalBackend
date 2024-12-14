using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TheOpenJournal.Models.DTOs;
using TheOpenJournal.Services.Interfaces;

namespace TheOpenJournal.Controllers
{
    [Authorize]
    [Route("api/v1/post")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;
        public PostController(IPostService postService)
        {
            _postService = postService;            
        }
        [HttpGet("index")]
        public IActionResult Index()
        {
            return Ok();
        }
        [HttpPost("add-post")]
        public async Task<IActionResult> AddPost(PostDTO postDto)
        {
            if (ModelState.IsValid)
            {
                return Ok(postDto);
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
