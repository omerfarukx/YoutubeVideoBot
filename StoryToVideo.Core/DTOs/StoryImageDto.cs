using System;

namespace StoryToVideo.Core.DTOs
{
    public class StoryImageDto
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; }
        public int Order { get; set; }
        public DateTime CreatedAt { get; set; }
    }
} 