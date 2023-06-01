using AutoMapper;
using Reco.Shared.Dtos.Folder;
using Reco.DAL.Entities;

namespace Reco.BLL.MappingProfiles
{
    public class FolderProfile : Profile
    {
        public FolderProfile()
        {
            CreateMap<NewFolderDTO, Folder>();
            CreateMap<Folder, FolderDTO>();
        }
    }
}
