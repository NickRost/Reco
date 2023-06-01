using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reco.DAL.Entities
{
    public class Comment
    {
        public int Id { get; set; }
        public string Body { get; set; }
        public int AuthorId { get; set; }
        public User Author { get; set; }
        public int VideoId { get; set; }
        public Video Video { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<CommentReaction> Reactions { get; set; }
    }
}
