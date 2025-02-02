namespace StoryToVideo.Core.Interfaces;

public interface IVideoProcessingService
{
    Task<string> CreateVideoAsync(List<string> imageUrls, string audioUrl);
    Task<bool> DeleteVideoAsync(string videoUrl);
} 