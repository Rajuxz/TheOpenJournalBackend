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
        [Column(TypeName ="text")]
        public string Content { get; set; }

        [Required]
        public string Slug { get; set; }
        public string? FeaturedImageUrl { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
       
       
        public ICollection<Category> Categories { get; set; } = new List<Category>();
        public virtual ICollection<Like> Likes { get; set; } = new List<Like>();


        [ForeignKey(nameof(UserModel))]
        public string UserId { get; set; }
        public virtual UserModel User { get; set; }

    }
    //Category model.
    public class Category
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        [Required]
        public string Name { get; set; }
        public ICollection<Post> Posts { get; set; } = new List<Post>();
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
    //Like Model
    public class Like
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public Guid PostId { get; set; }
        public DateTime LikedAt { get; set; } = DateTime.UtcNow;
        public virtual UserModel User { get; set; }
        public virtual Post Post { get; set; }
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
