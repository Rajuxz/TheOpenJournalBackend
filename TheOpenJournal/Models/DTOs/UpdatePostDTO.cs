namespace TheOpenJournal.Models.DTOs
{
    public class UpdatePostDTO
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Slug { get; set; }
        public string? FeaturedImageUrl { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public ICollection<CategoryDTO> Categories { get; set; } = new List<CategoryDTO>();
        public int LikeCount { get; set; } = 0;
        public int CommentCount { get; set; } = 0;
        public int UniqueViewCount { get; set; } = 0;
        public string UserId { get; set; }
    }
}
