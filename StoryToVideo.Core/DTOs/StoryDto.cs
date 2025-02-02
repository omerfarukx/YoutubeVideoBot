using StoryToVideo.Core.Entities;
using System;
using System.Collections.Generic;

namespace StoryToVideo.Core.DTOs
{
    public class StoryDto
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public required string Content { get; set; }
        public required string Status { get; set; }
        public string? ImageUrls { get; set; }
        public string? AudioUrl { get; set; }
        public string? VideoUrl { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public required string UserId { get; set; }
    }

    public class CreateStoryDto
    {
        public required string Title { get; set; }
        public required string Content { get; set; }
    }

    public class UpdateStoryDto
    {
        public required string Title { get; set; }
        public required string Content { get; set; }
    }
} 