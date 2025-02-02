using StoryToVideo.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StoryToVideo.Core.Interfaces
{
    public interface IStoryRepository : IGenericRepository<Story>
    {
        Task<IEnumerable<Story>> GetUserStoriesAsync(string userId);
        Task<Story> GetStoryWithDetailsAsync(int id);
        Task<IEnumerable<Story>> GetStoriesByStatusAsync(string status);
    }
} 