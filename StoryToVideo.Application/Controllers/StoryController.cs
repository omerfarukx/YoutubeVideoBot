using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StoryToVideo.Core.DTOs;
using StoryToVideo.Core.Entities;
using StoryToVideo.Core.Interfaces.Services;
using System.Security.Claims;

namespace StoryToVideo.Application.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class StoryController : ControllerBase
{
    private readonly IStoryService _storyService;
    private readonly IMapper _mapper;

    public StoryController(IStoryService storyService, IMapper mapper)
    {
        _storyService = storyService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<StoryDto>>> GetUserStories()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var stories = await _storyService.GetUserStoriesAsync(userId);
        return Ok(_mapper.Map<IEnumerable<StoryDto>>(stories));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<StoryDto>> GetStory(int id)
    {
        var story = await _storyService.GetStoryByIdAsync(id);
        if (story == null) return NotFound();

        if (story.UserId != User.FindFirst(ClaimTypes.NameIdentifier)?.Value)
            return Forbid();

        return Ok(_mapper.Map<StoryDto>(story));
    }

    [HttpPost]
    public async Task<ActionResult<StoryDto>> CreateStory(CreateStoryDto createStoryDto)
    {
        var story = _mapper.Map<Story>(createStoryDto);
        story.UserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        story.Status = "draft";

        var createdStory = await _storyService.CreateStoryAsync(story);
        return CreatedAtAction(nameof(GetStory), new { id = createdStory.Id }, 
            _mapper.Map<StoryDto>(createdStory));
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<StoryDto>> UpdateStory(int id, UpdateStoryDto updateStoryDto)
    {
        var story = await _storyService.GetStoryByIdAsync(id);
        if (story == null) return NotFound();

        if (story.UserId != User.FindFirst(ClaimTypes.NameIdentifier)?.Value)
            return Forbid();

        _mapper.Map(updateStoryDto, story);
        var updatedStory = await _storyService.UpdateStoryAsync(story);
        return Ok(_mapper.Map<StoryDto>(updatedStory));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteStory(int id)
    {
        var story = await _storyService.GetStoryByIdAsync(id);
        if (story == null) return NotFound();

        if (story.UserId != User.FindFirst(ClaimTypes.NameIdentifier)?.Value)
            return Forbid();

        var result = await _storyService.DeleteStoryAsync(id);
        return result ? NoContent() : BadRequest();
    }

    [HttpPost("{id}/process")]
    public async Task<ActionResult<StoryDto>> ProcessStory(int id)
    {
        var story = await _storyService.GetStoryByIdAsync(id);
        if (story == null) return NotFound();

        if (story.UserId != User.FindFirst(ClaimTypes.NameIdentifier)?.Value)
            return Forbid();

        try
        {
            story = await _storyService.ProcessStoryAsync(id);
            return Ok(_mapper.Map<StoryDto>(story));
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
} 