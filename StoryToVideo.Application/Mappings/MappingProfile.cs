using AutoMapper;
using StoryToVideo.Core.DTOs;
using StoryToVideo.Core.Entities;

namespace StoryToVideo.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Story, StoryDto>();
            CreateMap<CreateStoryDto, Story>();
            CreateMap<UpdateStoryDto, Story>();
            CreateMap<StoryImage, StoryImageDto>().ReverseMap();
            CreateMap<StoryAudio, StoryAudioDto>().ReverseMap();
            CreateMap<StoryVideo, StoryVideoDto>().ReverseMap();
        }
    }
} 