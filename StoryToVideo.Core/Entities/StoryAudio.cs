using System;

namespace StoryToVideo.Core.Entities
{
    public class StoryAudio
    {
        public int Id { get; set; }
        public string AudioUrl { get; set; }
        public string VoiceCharacter { get; set; }
        public DateTime CreatedAt { get; set; }
        
        public int StoryId { get; set; }
        public Story Story { get; set; }
    }
} 