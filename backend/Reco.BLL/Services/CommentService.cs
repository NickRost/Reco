using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Reco.DAL.Context;
using System.Collections.Generic;
using System.Linq;
using Reco.BLL.Services.Abstract;
using Reco.Shared.Dtos.Comment;
using Reco.DAL.Entities;

namespace Reco.BLL.Services
{
    public sealed class CommentService : BaseService
    {
        private readonly UserService _userService;
        public CommentService(RecoDbContext context, IMapper mapper, UserService userService) : base(context, mapper)
        {
            _userService = userService;
        }

        public async Task<CommentDTO> CreateComment(NewCommentDTO newComment)
        {
            var commentEntity = _mapper.Map<Comment>(newComment);

            _context.Comments.Add(commentEntity);
            await _context.SaveChangesAsync();
            return _mapper.Map<CommentDTO>(commentEntity);
        }

        public async Task DeleteComment(int commentId)
        {
            var commentEntity = await _context.Comments.FirstOrDefaultAsync(p => p.Id == commentId);
            _context.Comments.Remove(commentEntity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateComment(CommentDTO commentDto)
        {
            var commentEntity = await _context.Comments.FirstOrDefaultAsync(p => p.Id == commentDto.Id);

            commentEntity.Body = commentDto.Body;

            _context.Comments.Update(commentEntity);
            await _context.SaveChangesAsync();
        }

        public async Task<List<CommentDTO>> GetAllVideosComments(int videoId)
        {
            var allComments = await _context.Comments.Where(comment => comment.VideoId == videoId).ToListAsync();
            var allCommentsDTO = _mapper.Map<List<CommentDTO>>(allComments);
            return allCommentsDTO;
        }
    }
}
