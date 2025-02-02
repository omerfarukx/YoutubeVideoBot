using StoryToVideo.Core.Entities;

namespace StoryToVideo.Core.Interfaces
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User> GetByEmailAsync(string email);
        Task<User> GetUserWithStoriesAsync(int id);
    }
}