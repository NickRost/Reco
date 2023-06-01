using System;
using Reco.Shared.Enums;

namespace Reco.Shared.Dtos.Reactions
{
    public class CommentReactionDTO
    {
        public int Id { get; set; }
        public int CommentId { get; set; }
        public int UserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public Reaction Reaction { get; set; }
    }
}