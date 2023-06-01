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
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Email).IsRequired();
            builder.HasIndex(p => p.Email).IsUnique();
            builder.Property(p => p.Password).IsRequired();
            builder.Property(p => p.WorkspaceName).IsRequired();
            builder.Property(p => p.Salt).IsRequired();
            builder.HasMany(p => p.Permissions).WithMany(p => p.Users);
            builder.HasMany(p => p.Teams).WithMany(p => p.Users);
        }
    }
}
