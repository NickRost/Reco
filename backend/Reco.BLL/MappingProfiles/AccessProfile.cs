using AutoMapper;
using Reco.Shared.Dtos.Access;
using Reco.DAL.Entities;

namespace Reco.BLL.MappingProfiles
{
    public class AccessProfile : Profile
    {
        public AccessProfile()
        {
            CreateMap<AccessForRegisteredUsers, AccessForRegisteredUsersDTO>();
            CreateMap<AccessForRegisteredUsersDTO, AccessForRegisteredUsers>();
            CreateMap<AccessForUnregisteredUsers, AccessForUnregisteredUsersDTO>();
            CreateMap<AccessForUnregisteredUsersDTO, AccessForUnregisteredUsers>();
        }
    }
}
