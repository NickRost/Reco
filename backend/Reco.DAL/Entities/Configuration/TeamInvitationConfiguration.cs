using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reco.DAL.Entities.Configuration
{
    public class TeamInvitationConfiguration : IEntityTypeConfiguration<TeamInvitation>
    {
        public void Configure(EntityTypeBuilder<TeamInvitation> builder)
        {
            builder.HasKey(p => new { p.TeamId, p.UserId });
            builder.HasOne<User>().WithMany().HasForeignKey(p => p.UserId);
            builder.HasOne<Team>().WithMany().HasForeignKey(p => p.TeamId);
        }
    }
}
