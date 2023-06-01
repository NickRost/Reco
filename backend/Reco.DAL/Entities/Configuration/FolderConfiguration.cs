using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Reco.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reco.DAL.Entities.Configuration
{
    public class FolderConfiguration : IEntityTypeConfiguration<Folder>
    {
        public void Configure(EntityTypeBuilder<Folder> builder)
        {
            builder.HasKey(p => p.Id);
            builder.HasOne(p => p.Team).WithMany().HasForeignKey(p => p.TeamId);
            builder.HasOne(p => p.Author).WithMany().HasForeignKey(p => p.AuthorId);
            builder.Property(p => p.Name).IsRequired();
        }
    }
}
