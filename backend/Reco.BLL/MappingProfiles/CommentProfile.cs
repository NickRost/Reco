using AutoMapper;
using Reco.Shared.Dtos.Comment;
using Reco.DAL.Entities;

namespace Reco.BLL.MappingProfiles
{
    public class CommentProfile : Profile
    {
        public CommentProfile()
        {
            CreateMap<NewCommentDTO, CommentDTO>();
            CreateMap<NewCommentDTO, Comment>();
            CreateMap<Comment, CommentDTO>();
        }
    }
}
