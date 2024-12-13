using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace TheOpenJournal.Models.Domain
{
    public class Post
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        [Required]
        public string Title { get; set; }
        [Required]
        public string Content { get; set; }

        [Required]
        public string Slug { get; set; }
        public string? FeaturedImageUrl { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        [Required]
        public ICollection<Tag> Tags { get; set; } = new List<Tag>();
        [Required]

        [ForeignKey(nameof(Category))]
        public Guid CategoryId { get; set; }
        public virtual Category Category { get; set; }
        public int LikeCount { get; set; } = 0;
        public int CommentCount { get; set; } = 0;
        public int UniqueViewCount { get; set; } = 0;
    }
    //Category model.
    public class Category
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        [Required]
        public string Name { get; set; }

    }
    //Comment model.
    public class Comment
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        [ForeignKey(nameof(Post))]
        public Guid PostId { get; set; }
        public virtual Post Posts { get; set; }

        [ForeignKey(nameof(UserModel))]
        public string UserId { get; set; }
        public UserModel User { get; set; }
        [Required]
        public string CommentedText { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
    //Tag model.
    public class Tag
    {
        public int Id { get; set; } 
        [Required]
        public string Name { get; set; }

        [JsonIgnore]
        public ICollection<Post> Posts { get; set; } = new List<Post>();
    }
}
