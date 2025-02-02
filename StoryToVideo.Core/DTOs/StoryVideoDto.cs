using System;

namespace StoryToVideo.Core.DTOs
{
    public class StoryVideoDto
    {
        public int Id { get; set; }
        public string VideoUrl { get; set; }
        public string Format { get; set; }
        public DateTime CreatedAt { get; set; }
    }
} 