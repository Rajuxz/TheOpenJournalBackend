using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TheOpenJournal.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/v1/likes")]
    public class LikeController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult> GetLikes()
        {
            return Ok();
        }
        [HttpPost]
        public async Task<ActionResult> AddLikes([FromForm]Guid id)
        {
            return Ok(id);
        }
        [HttpPatch]
        public async Task<ActionResult> UpdateLikes([FromForm] Guid id)
        {
            return Ok(id);
        }
        
    }
}
