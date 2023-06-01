using AutoMapper;
using Reco.Shared.Dtos.Reactions;
using Reco.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reco.BLL.MappingProfiles
{
    public class VideoReactionProfile : Profile
    {
        public VideoReactionProfile()
        {
            CreateMap<NewVideoReactionDTO, VideoReaction>();
            CreateMap<VideoReaction, VideoReactionDTO>();
        }
    }
}
