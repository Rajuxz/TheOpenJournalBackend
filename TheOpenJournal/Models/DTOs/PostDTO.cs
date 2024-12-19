using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using TheOpenJournal.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace TheOpenJournal.Models.DTOs
{
    public class PostDTO
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Content { get; set; }
        [Required]
        public string Slug { get; set; }
        public IFormFile? FeaturedImage { get; set; }
        public List<Guid> CategoryId { get; set; } 
        public string? User { get; set; }
    }

    // PostDTO for Retrieving Posts.
    class GetPostDTO
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string Slug { get; set; }
        public string? FeaturedImageUrl { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        public ICollection<Category> Categories { get; set; } = new List<Category>();
        public int LikeCount { get; set; } = 0;
        public int CommentCount { get; set; } = 0;
        public int UniqueViewCount { get; set; } = 0;
        public string UserId { get; set; }
    }
}
