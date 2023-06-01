using AutoMapper;
using Reco.Shared.Dtos;
using Reco.DAL.Entities;

namespace Reco.BLL.MappingProfiles
{
    public class VideoProfiles : Profile
    {
        public VideoProfiles()
        {
            CreateMap<VideoReaction, VideoReactionDTO>();
            CreateMap<VideoReactionDTO, VideoReaction>();
            CreateMap<Video, VideoDTO>();
        }
    }
}
