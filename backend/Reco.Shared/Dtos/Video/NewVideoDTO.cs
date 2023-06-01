using Reco.Shared.Dtos.Comment;
using Reco.Shared.Dtos.Reactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reco.Shared.Dtos.Video
{
    public class NewVideoDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int UserId { get; set; }
        public int? FolderId { get; set; }
        public string Link { get; set; }
        public List<VideoReactionDTO> Reactions { get; set; }
    }
}
