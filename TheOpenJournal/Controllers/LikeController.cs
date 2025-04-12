using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TheOpenJournal.Models.DTOs;
using TheOpenJournal.Services.Interfaces;

namespace TheOpenJournal.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/v1/likes")]
    public class LikeController : ControllerBase
    {
        private readonly ILikeService _likeService;
        public LikeController(ILikeService likeService)
        {
            _likeService = likeService;            
        }
        [HttpGet]
        public async Task<ActionResult> GetLikes()
        {
            return Ok();
        }
        [HttpPost]
        public async Task<ActionResult> AddLikes([FromBody] AddLikeDto likeDto)
        {
            if (!ModelState.IsValid) {
                return BadRequest(new { message = "Invalid Id given." });
            }
            //Retrieve userEmail from 'sub'
            var userEmail = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            //send the userEmail and postId to the service.
            bool succeed = await _likeService.LikePostAsync(userEmail, likeDto.PostId);
            if (!succeed)
            {
                return BadRequest(new { message = "Cannot like" });
            }

            return Ok();
        }
        [HttpPatch]
        public async Task<ActionResult> UpdateLikes([FromForm] Guid id)
        {
            return Ok(id);
        }
        
    }
}
