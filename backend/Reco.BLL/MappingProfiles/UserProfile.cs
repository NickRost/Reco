using AutoMapper;
using Reco.Shared.Dtos.User;
using Reco.DAL.Entities;

namespace Reco.BLL.MappingProfiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<NewUserDTO, User>();
            CreateMap<User, UserDTO>();
        }
    }
}
