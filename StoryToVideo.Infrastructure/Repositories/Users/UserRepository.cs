using Microsoft.EntityFrameworkCore;
using StoryToVideo.Core.Interfaces;
using StoryToVideo.Core.Entities;
using StoryToVideo.Infrastructure.Data;

namespace StoryToVideo.Infrastructure.Repositories.Users
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User> GetUserWithStoriesAsync(int id)
        {
            return await _context.Users
                .Include(u => u.Stories)
                .FirstOrDefaultAsync(u => u.Id == id);
        }
    }
}