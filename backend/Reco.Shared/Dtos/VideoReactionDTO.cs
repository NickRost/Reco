using Reco.Shared.Enums;
using System;

namespace Reco.Shared.Dtos
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
