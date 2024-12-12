using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography.X509Certificates;

namespace TheOpenJournal.Controllers
{
    [Route("api/v1/blog")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        [HttpGet]
        public IActionResult AddBlog()
        {
            try
            {
                return Ok(new { message = "Post Added Successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }           
    }
}
