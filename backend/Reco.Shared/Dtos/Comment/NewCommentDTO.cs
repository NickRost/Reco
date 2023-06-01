using Reco.Shared.Dtos.User;
using System;
using System.Collections.Generic;
using Reco.Shared.Dtos.Reactions;

namespace Reco.Shared.Dtos.Comment
{
    public class NewCommentDTO
    {
        public int AuthorId { get; set; }
        public int VideoId { get; set; }
        public string Body { get; set; }
        public ICollection<NewCommentReactionDTO> Reactions { get; set; }
    }
}