using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StoryToVideo.Core.Entities
{
    public class Story
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [StringLength(200)]
        public string Title { get; set; }
        
        [Required]
        public string Content { get; set; }
        
        [Required]
        public string Status { get; set; } = "draft"; // draft, processing, completed
        
        public string? ImageUrls { get; set; } // JSON array string
        public string? AudioUrl { get; set; }
        public string? VideoUrl { get; set; }
        
        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        
        [Required]
        public string UserId { get; set; }
        
        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }
        
        // Navigation properties
        public virtual ICollection<StoryImage> Images { get; set; }
        public virtual StoryAudio Audio { get; set; }
        public virtual StoryVideo Video { get; set; }
    }

    public enum StoryStatus
    {
        Draft,
        Processing,
        Completed
    }
} 