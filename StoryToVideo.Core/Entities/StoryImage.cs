using System;

namespace StoryToVideo.Core.Entities
{
    public class StoryImage
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; }
        public int Order { get; set; }
        public DateTime CreatedAt { get; set; }
        
        public int StoryId { get; set; }
        public Story Story { get; set; }
    }
} 