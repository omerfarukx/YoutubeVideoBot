using System;

namespace StoryToVideo.Core.Entities
{
    public class StoryVideo
    {
        public int Id { get; set; }
        public string VideoUrl { get; set; }
        public string Format { get; set; }
        public DateTime CreatedAt { get; set; }
        
        public int StoryId { get; set; }
        public Story Story { get; set; }
    }
} 