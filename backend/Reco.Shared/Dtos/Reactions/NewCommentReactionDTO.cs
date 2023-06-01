using Reco.Shared.Enums;

namespace Reco.Shared.Dtos.Reactions
{
    public class NewCommentReactionDTO
    {
        public int CommentId { get; set; }
        public int UserId { get; set; }
        public Reaction Reaction { get; set; }
    }
}