using System.Collections.Generic;

namespace Reco.Shared.Dtos.Folder
{
    public class FolderDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int AuthorId { get; set; }
        public int? ParentId { get; set; }
        public int? TeamId { get; set; }
    }
}
