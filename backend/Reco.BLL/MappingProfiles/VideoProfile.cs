using AutoMapper;
using Reco.Shared.Dtos;
using Reco.Shared.Dtos.User;
using Reco.Shared.Dtos.Video;
using Reco.DAL.Entities;

namespace Reco.BLL.MappingProfiles
{
    public class VideoProfile : Profile
    {
        public VideoProfile()
        {
            CreateMap<Video, VideoDTO>();
            CreateMap<NewVideoDTO, Video>()
                .ForMember(video => video.Reactions, act => act.Ignore())
                .ForMember(video => video.AuthorId, conf => conf.MapFrom(arg => arg.UserId));
        }
    }
}
