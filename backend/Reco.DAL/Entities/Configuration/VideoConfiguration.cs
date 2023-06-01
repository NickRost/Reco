using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reco.DAL.Entities.Configuration
{
    public class VideoConfiguration : IEntityTypeConfiguration<Video>
    {
        public void Configure(EntityTypeBuilder<Video> builder)
        {
            builder.HasKey(p => p.Id);
            builder.HasOne(p => p.Author).WithMany().HasForeignKey(p => p.AuthorId);
            builder.Property(p => p.Link).IsRequired();
            builder.Property(p => p.Name).IsRequired();
            builder.OwnsMany(p => p.Reactions, r =>
            {
                r.HasKey(x => x.Id);
                r.WithOwner().HasForeignKey(x => x.VideoId);
                r.HasOne<User>().WithMany().HasForeignKey(x => x.UserId);
                r.Property(x => x.Reaction).IsRequired();
                r.Property(x => x.CreatedAt).IsRequired();
            });
            builder.HasOne<Folder>().WithMany().HasForeignKey(p => p.FolderId);
        }
    }
}
