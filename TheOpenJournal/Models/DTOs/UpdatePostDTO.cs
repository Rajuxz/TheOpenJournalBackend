namespace TheOpenJournal.Models.DTOs
{
    public class UpdatePostDTO
    {
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public string? Slug { get; set; }
        public string? FeaturedImageUrl { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public ICollection<UpdatePostCategoryDto>? Categories { get; set; } = new List<UpdatePostCategoryDto>();
        public int? LikeCount { get; set; } = 0;
        public int? CommentCount { get; set; } = 0;
        public int? UniqueViewCount { get; set; } = 0;
        public string? UserId { get; set; }
    }

    // To Update Categories during Post Update
    public class UpdatePostCategoryDto
    {
        public Guid Id { get; set; }
    }
}
