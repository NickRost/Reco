using System.Collections.Generic;

namespace Reco.DAL.Entities
{
    public class Folder
    {
        public int Id { get; set; }
        public int? ParentId { get; set; }
        public string Name { get; set; }
        public int AuthorId { get; set; }
        public User Author { get; set; }
        public int? TeamId { get; set; }
        public Team Team { get; set; }
        public ICollection<Folder> SubFolders { get; } = new List<Folder>();
        public ICollection<Video> Videos { get; set; }
    }
}
