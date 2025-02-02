using Microsoft.EntityFrameworkCore;
using StoryToVideo.Core.Entities;
using StoryToVideo.Core.Interfaces;
using StoryToVideo.Infrastructure.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StoryToVideo.Infrastructure.Repositories
{
    public class StoryRepository : GenericRepository<Story>, IStoryRepository
    {
        private readonly ApplicationDbContext _context;

        public StoryRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Story>> GetUserStoriesAsync(string userId)
        {
            return await _context.Stories
                .Where(s => s.UserId == userId)
                .Include(s => s.Images)
                .Include(s => s.Audio)
                .Include(s => s.Video)
                .OrderByDescending(s => s.CreatedAt)
                .ToListAsync();
        }

        public async Task<Story> GetStoryWithDetailsAsync(int id)
        {
            return await _context.Stories
                .Include(s => s.Images)
                .Include(s => s.Audio)
                .Include(s => s.Video)
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<IEnumerable<Story>> GetStoriesByStatusAsync(string status)
        {
            return await _context.Stories
                .Where(s => s.Status == status)
                .Include(s => s.Images)
                .Include(s => s.Audio)
                .Include(s => s.Video)
                .OrderByDescending(s => s.CreatedAt)
                .ToListAsync();
        }
    }
} 