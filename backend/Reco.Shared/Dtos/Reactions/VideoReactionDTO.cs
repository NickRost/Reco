using System;
using Reco.Shared.Enums;

namespace Reco.Shared.Dtos.Reactions
{
    public class VideoReactionDTO
    {
        public int Id { get; set; }
        public int VideoId { get; set; }
        public int UserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public Reaction Reaction { get; set; }
    }
}