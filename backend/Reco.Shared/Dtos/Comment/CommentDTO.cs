using Reco.Shared.Dtos.User;
using System;
using System.Collections.Generic;
using Reco.Shared.Dtos.Reactions;

namespace Reco.Shared.Dtos.Comment
{
    public class CommentDTO
    {
        public int Id { get; set; }
        public int VideoId { get; set; }
        public int AuthorId { get; set; }
        public DateTime CreatedAt { get; set; }
        public ICollection<CommentReactionDTO> Reactions { get; set; }
        public string Body { get; set; }
    }
}