using AutoMapper;
using Reco.Shared.Dtos;
using Reco.DAL.Entities;

namespace Reco.BLL.MappingProfiles
{
    public class TeamProfile : Profile
    {
        public TeamProfile()
        {
            CreateMap<Team, TeamDTO>()
                .ForMember(m => m.MemberCount, opt => opt.MapFrom(src => src.Users.Count));
        }
    }
}
