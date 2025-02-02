using StoryToVideo.Core.Interfaces;

namespace StoryToVideo.Infrastructure.Services;

public class FFmpegVideoService : IVideoProcessingService
{
    public async Task<string> CreateVideoAsync(List<string> imageUrls, string audioUrl)
    {
        // FFmpeg implementation will go here
        throw new NotImplementedException();
    }

    public async Task<bool> DeleteVideoAsync(string videoUrl)
    {
        // Delete implementation
        throw new NotImplementedException();
    }
} 