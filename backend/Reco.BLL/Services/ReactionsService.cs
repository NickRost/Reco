using AutoMapper;
using System.Linq;
using System.Threading.Tasks;
using Reco.Shared.Enums;
using Reco.DAL.Context;
using Reco.BLL.Services.Abstract;
using Reco.Shared.Dtos.Reactions;
using Reco.DAL.Entities;

namespace Reco.BLL.Services
{
    public sealed class ReactionService : BaseService
    {
        public ReactionService(RecoDbContext context, IMapper mapper) : base(context, mapper)
        { }

        public async Task ReactVideo(NewVideoReactionDTO reaction)
        {
            var video = await _context.Videos.FindAsync(reaction.VideoId);
            var userReaction = video.Reactions.FirstOrDefault(x => x.UserId == reaction.UserId);
            if (userReaction != null)
            {
                video.Reactions.Remove(userReaction);
                await _context.SaveChangesAsync();
                if (userReaction.Reaction == reaction.Reaction)
                {
                    return;
                }
            }

            var newReaction = _mapper.Map<VideoReaction>(reaction);
            video.Reactions.Add(newReaction);
            await _context.SaveChangesAsync();
        }

        public async Task ReactComment(NewCommentReactionDTO reaction)
        {
            var comment = await _context.Comments.FindAsync(reaction.CommentId);
            var userReaction = comment.Reactions.FirstOrDefault(x => x.UserId == reaction.UserId);
            if (userReaction != null)
            {
                comment.Reactions.Remove(userReaction);
                await _context.SaveChangesAsync();
                if (userReaction.Reaction == reaction.Reaction)
                {
                    return;
                }
            }

            var newReaction = _mapper.Map<CommentReaction>(reaction);
            comment.Reactions.Add(newReaction);
            await _context.SaveChangesAsync();
        }
    }
}
