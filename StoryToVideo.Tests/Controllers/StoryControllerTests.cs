using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using StoryToVideo.Application.Controllers;
using StoryToVideo.Core.DTOs;
using StoryToVideo.Core.Entities;
using StoryToVideo.Core.Interfaces.Services;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Xunit;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace StoryToVideo.Tests.Controllers;

public class StoryControllerTests
{
    private readonly Mock<IStoryService> _mockStoryService;
    private readonly Mock<IMapper> _mockMapper;
    private readonly StoryController _controller;

    public StoryControllerTests()
    {
        _mockStoryService = new Mock<IStoryService>();
        _mockMapper = new Mock<IMapper>();
        _controller = new StoryController(_mockStoryService.Object, _mockMapper.Object);

        // Test için user context'i oluştur
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, "test-user-id")
        };
        var identity = new ClaimsIdentity(claims);
        var claimsPrincipal = new ClaimsPrincipal(identity);
        _controller.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext { User = claimsPrincipal }
        };
    }

    [Fact]
    public async Task CreateStory_ValidInput_ReturnsCreatedResponse()
    {
        // Arrange
        var createDto = new CreateStoryDto { Title = "Test Story", Content = "Test Content" };
        var story = new Story { Id = 1, Title = "Test Story", Content = "Test Content", UserId = "test-user-id" };
        var storyDto = new StoryDto 
        { 
            Id = 1, 
            Title = "Test Story", 
            Content = "Test Content",
            Status = "draft",
            UserId = "test-user-id",
            CreatedAt = DateTime.UtcNow
        };

        _mockMapper.Setup(m => m.Map<Story>(createDto)).Returns(story);
        _mockMapper.Setup(m => m.Map<StoryDto>(story)).Returns(storyDto);
        _mockStoryService.Setup(s => s.CreateStoryAsync(It.IsAny<Story>())).ReturnsAsync(story);

        // Act
        var result = await _controller.CreateStory(createDto);

        // Assert
        var createdResult = Assert.IsType<CreatedAtActionResult>(result.Result);
        var returnValue = Assert.IsType<StoryDto>(createdResult.Value);
        Assert.Equal("Test Story", returnValue.Title);
    }

    [Fact]
    public async Task GetUserStories_ReturnsStoriesList()
    {
        // Arrange
        var stories = new List<Story> 
        { 
            new Story { Id = 1, Title = "Story 1", UserId = "test-user-id", Status = "draft" },
            new Story { Id = 2, Title = "Story 2", UserId = "test-user-id", Status = "draft" }
        };
        var storyDtos = stories.Select(s => new StoryDto 
        { 
            Id = s.Id, 
            Title = s.Title, 
            Status = s.Status,
            UserId = s.UserId,
            Content = "Test Content",
            CreatedAt = DateTime.UtcNow
        }).ToList();

        _mockStoryService.Setup(s => s.GetUserStoriesAsync("test-user-id")).ReturnsAsync(stories);
        _mockMapper.Setup(m => m.Map<IEnumerable<StoryDto>>(stories)).Returns(storyDtos);

        // Act
        var result = await _controller.GetUserStories();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnValue = Assert.IsType<List<StoryDto>>(okResult.Value);
        Assert.Equal(2, returnValue.Count);
    }
} 