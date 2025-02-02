using StoryToVideo.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StoryToVideo.Core.Interfaces.Services
{
    public interface IStoryService
    {
        Task<IEnumerable<Story>> GetUserStoriesAsync(string userId);
        Task<Story> GetStoryByIdAsync(int id);
        Task<Story> CreateStoryAsync(Story story);
        Task<Story> UpdateStoryAsync(Story story);
        Task<bool> DeleteStoryAsync(int id);
        Task<IEnumerable<Story>> GetStoriesByStatusAsync(string status);
        Task<Story> UpdateStoryStatusAsync(int id, string status);
        Task<Story> ProcessStoryAsync(int id);
    }
}