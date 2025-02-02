using StoryToVideo.Core.Entities;
using StoryToVideo.Core.Interfaces;
using StoryToVideo.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StoryToVideo.Application.Services
{
    public class StoryService : IStoryService
    {
        private readonly IStoryRepository _storyRepository;
        private readonly IAIService _aiService;

        public StoryService(IStoryRepository storyRepository, IAIService aiService)
        {
            _storyRepository = storyRepository;
            _aiService = aiService;
        }

        public async Task<Story> GetStoryByIdAsync(int id)
        {
            return await _storyRepository.GetStoryWithDetailsAsync(id);
        }

        public async Task<IEnumerable<Story>> GetUserStoriesAsync(string userId)
        {
            return await _storyRepository.GetUserStoriesAsync(userId);
        }

        public async Task<Story> CreateStoryAsync(Story story)
        {
            await _storyRepository.AddAsync(story);
            return story;
        }

        public async Task<Story> UpdateStoryAsync(Story story)
        {
            story.UpdatedAt = DateTime.UtcNow;
            await _storyRepository.UpdateAsync(story);
            return story;
        }

        public async Task<bool> DeleteStoryAsync(int id)
        {
            var story = await _storyRepository.GetByIdAsync(id);
            if (story == null) return false;
            
            await _storyRepository.DeleteAsync(story);
            return true;
        }

        public async Task<IEnumerable<Story>> GetStoriesByStatusAsync(string status)
        {
            return await _storyRepository.GetStoriesByStatusAsync(status);
        }

        public async Task<Story> UpdateStoryStatusAsync(int id, string status)
        {
            var story = await _storyRepository.GetByIdAsync(id);
            if (story == null) throw new KeyNotFoundException($"Story with id {id} not found");

            story.Status = status;
            story.UpdatedAt = DateTime.UtcNow;
            await _storyRepository.UpdateAsync(story);
            
            return story;
        }

        public async Task<Story> ProcessStoryAsync(int id)
        {
            var story = await _storyRepository.GetStoryWithDetailsAsync(id);
            if (story == null) throw new Exception("Story not found");

            story.Status = "processing";
            await _storyRepository.UpdateAsync(story);

            try
            {
                // Generate images
                var imageUrls = await _aiService.GenerateImagesAsync(story.Content, 5);
                story.ImageUrls = System.Text.Json.JsonSerializer.Serialize(imageUrls);
                
                // Generate audio
                var audioUrl = await _aiService.GenerateAudioAsync(story.Content, "default");
                story.AudioUrl = audioUrl;
                
                // Generate video
                var videoUrl = await _aiService.GenerateVideoAsync(story.Id, imageUrls, audioUrl);
                story.VideoUrl = videoUrl;

                story.Status = "completed";
                story.UpdatedAt = DateTime.UtcNow;
                await _storyRepository.UpdateAsync(story);

                return story;
            }
            catch (Exception)
            {
                story.Status = "draft";
                await _storyRepository.UpdateAsync(story);
                throw;
            }
        }
    }
} 