using Reco.Shared.Enums;

namespace Reco.Shared.Dtos.Reactions
{
    public class NewVideoReactionDTO
    {
        public int VideoId { get; set; }
        public int UserId { get; set; }
        public Reaction Reaction { get; set; }
    }
}