using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using TheOpenJournal.Models.DTOs;
using TheOpenJournal.Services.Interfaces;

namespace TheOpenJournal.Controllers
{
    [Route("api/v1/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthServices _authService;
        public AuthController(IAuthServices authService)
        {
            _authService = authService;            
        }
        [HttpPost("signup")]
        public async Task<IActionResult> SignUp([FromBody] UserDTO userDTO)
        {
            if (ModelState.IsValid)
            {
                var (succeeded,message)= await _authService.CreateUserAsync(userDTO);
                if (succeeded)
                {
                    string token = _authService.GenerateToken(userDTO.Username, userDTO.Email, "User");
                    var cookieOptions = new CookieOptions
                    {
                        HttpOnly = true,
                        Secure = true,
                        SameSite = SameSiteMode.None,
                        Expires = DateTime.UtcNow.AddDays(30),
                    };
                    Response.Cookies.Append("Jwt", token, cookieOptions);
                    return Ok( new { message = "Account Created Successfully!" });
                }

                return BadRequest(new { message = message });
            }
            return BadRequest( new {message="Invalid Model State"});
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserDTO userDTO)
        {
            if (ModelState.IsValid)
            {
                var (succeeded, message, token) = await _authService.LoginAsync(userDTO);
                if (succeeded)
                {
                    var cookieOptions = new CookieOptions
                    {
                        HttpOnly = true,
                        Secure = true,
                        SameSite = SameSiteMode.None,
                        Expires = DateTime.UtcNow.AddDays(30)
                    };
                    Response.Cookies.Append("Jwt", token, cookieOptions);
                    return Ok(new {message = "Login Successful !"});
                }
                else
                {
                    return BadRequest(new {isSuccess = false, errorMessage = message });
                }
            }
            else
            {
                return BadRequest(new { isSuccess = false, message = "Invalid Data Given !" });
            }
        }

        [HttpGet("checkuser")]
        [Authorize]
        public IActionResult UserStatus()
        {
            var username = User.Identity?.Name;
            return Ok(new {isAuthenticated = true, username = username});
        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("Jwt");
            return Ok(new { message = "Log out Successful." });
        }
    }
}
