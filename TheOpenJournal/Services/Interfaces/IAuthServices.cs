using TheOpenJournal.Models.DTOs;

namespace TheOpenJournal.Services.Interfaces
{
    public interface IAuthServices
    {
        string GenerateToken(string username,string email, string role);
        Task<(bool Succeed, string? Message)> CreateUserAsync(UserDTO userDTO);
        Task<(bool Succeed, string? Message,string? Token)> LoginAsync(UserDTO userDTO);
    }
}
