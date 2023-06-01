using System.Collections.Generic;

namespace Reco.DAL.Entities
{
    public class Team
    {
        public int Id { get; set; }
        public int AuthorId { get; set; }
        public string Name { get; set; }
        public List<User> Users { get; set; }
    }
}
