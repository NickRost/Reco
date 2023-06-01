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
    public class CommentConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Body).IsRequired();
            builder.HasOne(p => p.Author).WithMany().HasForeignKey(p => p.AuthorId).IsRequired();
            builder.OwnsMany(p => p.Reactions, r =>
            {
                r.HasKey(x => x.Id);
                r.WithOwner().HasForeignKey(x => x.CommentId);
                r.HasOne(x => x.User).WithMany().HasForeignKey(x => x.UserId);
                r.Property(x => x.Reaction).IsRequired();
                r.Property(x => x.CreatedAt).IsRequired();
            });
            builder.HasOne<Video>().WithMany().HasForeignKey(p => p.VideoId);
        }
    }
}
