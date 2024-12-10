using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TheOpenJournal.Models.Domain;
using TheOpenJournal.Models.DTOs;
using TheOpenJournal.Services.Interfaces;

namespace TheOpenJournal.Services.Implementation
{
    public class AuthServices:IAuthServices
    {
        public readonly IConfiguration _configuration;
        private readonly UserManager<UserModel> _userManager;
        public AuthServices(IConfiguration configuration, UserManager<UserModel> userManager)
        {
            _configuration = configuration;
            _userManager = userManager;
        }
        public string GenerateToken(string username, string email, string role)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.Role,role)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims:claims,
                expires:DateTime.Now.AddDays(7),
                signingCredentials:credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }


        public async Task<(bool Succeed, string? Message)> CreateUserAsync(UserDTO userDTO)
        {
            var user = new UserModel
            {
                UserName = userDTO.Username,
                Email = userDTO.Email
            };
            // Usermanager handles hashing and storing the passwords.
            var result = await _userManager.CreateAsync(user,userDTO.Password);
            if (result.Succeeded)
            {
                return (true,null);
            }

            var errorMessage = string.Join(";", result.Errors.Select(e=>e.Description));
            return (false, errorMessage);
        }

        public async Task<(bool Succeed, string? Message,string? Token)> LoginAsync(UserDTO userDTO)
        {
            var user = await _userManager.FindByEmailAsync(userDTO.Email);
            if (user == null)
            {
                return (false, "Account not found.",null);
            }
            else
            {
                if(! await _userManager.CheckPasswordAsync(user, userDTO.Password))
                {
                    return (false, "Password Doesnot matched.",null);
                }
                else
                {
                    string token = GenerateToken(userDTO.Username??userDTO.Email,userDTO.Email,"user");
                    return (true, null,token); 
                }
            }
        }
    }
}
