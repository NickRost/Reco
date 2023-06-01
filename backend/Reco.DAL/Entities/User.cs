using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reco.DAL.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string WorkspaceName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsEmailConfirmed { get; set; }
        public DateTime CreatedAt { get; set; }
        public string AvatarLink { get; set; }
        public string Salt { get; set; }
        public List<Permission> Permissions { get; set; }
        public List<Team> Teams { get; set; }
    }
}
