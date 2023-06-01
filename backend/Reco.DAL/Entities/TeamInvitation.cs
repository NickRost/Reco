using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reco.DAL.Entities
{
    public class TeamInvitation
    {
        public int UserId { get; set; }
        public int TeamId { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsAccepted { get; set; }
    }
}
