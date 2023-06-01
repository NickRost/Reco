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
    public class NotificationConfiguration : IEntityTypeConfiguration<Notification>
    {
        public void Configure(EntityTypeBuilder<Notification> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Message).IsRequired();
            builder.Property(p => p.ReceiverId).IsRequired();
            builder.HasOne<User>().WithMany().HasForeignKey(p => p.ReceiverId);
            builder.Property(p => p.CreatedAt).IsRequired();
            builder.Property(p => p.Type).IsRequired();
            builder.Property(p => p.IsRead).IsRequired();
        }
    }
}
