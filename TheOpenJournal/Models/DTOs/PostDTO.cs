﻿using System.ComponentModel.DataAnnotations.Schema;
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
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        List<Guid> CategoryId { get; set; } = new List<Guid>();
    }
}
