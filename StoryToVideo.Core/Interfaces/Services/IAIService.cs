using System.Threading.Tasks;
using System.Collections.Generic;

namespace StoryToVideo.Core.Interfaces.Services
{
    public interface IAIService
    {
        Task<List<string>> GenerateImagesAsync(string prompt, int count);
        Task<string> GenerateAudioAsync(string text, string voiceCharacter);
        Task<string> GenerateVideoAsync(int storyId, List<string> imageUrls, string audioUrl);
    }
}