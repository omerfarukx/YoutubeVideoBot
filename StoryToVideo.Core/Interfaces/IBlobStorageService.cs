namespace StoryToVideo.Core.Interfaces;

public interface IBlobStorageService
{
    Task<string> UploadAsync(Stream content, string fileName, string contentType);
    Task<bool> DeleteAsync(string fileName);
    Task<Stream> DownloadAsync(string fileName);
} 