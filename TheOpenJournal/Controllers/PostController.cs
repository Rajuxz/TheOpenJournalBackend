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
        public async Task<IActionResult> AddPost([FromForm]PostDTO postDto)
        {
            if (ModelState.IsValid)
            {
                postDto.User = User.Identity.Name;
                var res = _postService.AddPostAsync(postDto);
                return Ok(res);
            }
            else
            {
                return BadRequest(new { message ="Please provide all the data."});

            }
        }
    }
}
