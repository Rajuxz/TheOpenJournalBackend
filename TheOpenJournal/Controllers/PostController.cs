using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
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
                var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
                postDto.User = userId;
                var res = _postService.AddPostAsync(postDto);
                return Ok(new { res, userId });
            }
            else
            {
                return BadRequest(new { message ="Please provide all the data."});

            }
        }
    }
}
