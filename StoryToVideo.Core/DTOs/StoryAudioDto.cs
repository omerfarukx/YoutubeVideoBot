using System;

namespace StoryToVideo.Core.DTOs
{
    public class StoryAudioDto
    {
        public int Id { get; set; }
        public string AudioUrl { get; set; }
        public string VoiceCharacter { get; set; }
        public DateTime CreatedAt { get; set; }
    }
} 