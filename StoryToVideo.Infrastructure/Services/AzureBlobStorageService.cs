using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using StoryToVideo.Core.Interfaces;
using Microsoft.Extensions.Configuration;

namespace StoryToVideo.Infrastructure.Services;

public class AzureBlobStorageService : IBlobStorageService
{
    private readonly BlobServiceClient _blobServiceClient;
    private readonly string _containerName;

    public AzureBlobStorageService(IConfiguration configuration)
    {
        _blobServiceClient = new BlobServiceClient(configuration["AzureStorage:ConnectionString"]);
        _containerName = configuration["AzureStorage:ContainerName"];
    }

    public async Task<string> UploadAsync(Stream content, string fileName, string contentType)
    {
        var containerClient = _blobServiceClient.GetBlobContainerClient(_containerName);
        var blobClient = containerClient.GetBlobClient(fileName);
        await blobClient.UploadAsync(content, new BlobHttpHeaders { ContentType = contentType });
        return blobClient.Uri.ToString();
    }

    public async Task<bool> DeleteAsync(string fileName)
    {
        var containerClient = _blobServiceClient.GetBlobContainerClient(_containerName);
        var blobClient = containerClient.GetBlobClient(fileName);
        return await blobClient.DeleteIfExistsAsync();
    }

    public async Task<Stream> DownloadAsync(string fileName)
    {
        var containerClient = _blobServiceClient.GetBlobContainerClient(_containerName);
        var blobClient = containerClient.GetBlobClient(fileName);
        var response = await blobClient.DownloadAsync();
        return response.Value.Content;
    }
} 