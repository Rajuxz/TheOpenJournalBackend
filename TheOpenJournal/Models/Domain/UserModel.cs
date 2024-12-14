using Microsoft.AspNetCore.Identity;
namespace TheOpenJournal.Models.Domain
{
    public class UserModel:IdentityUser
    {
        
        public string? ProfilePicture{get;set;}
        public string? Bio { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public virtual ICollection<Post> Posts { get; set; } = new List<Post>();
    }
}
