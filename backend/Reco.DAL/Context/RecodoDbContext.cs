using Microsoft.EntityFrameworkCore;
using Reco.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reco.DAL.Context
{
    public class RecoDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<Team> Teams  { get; set; }
        public DbSet<Video> Videos { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<TeamInvitation> TeamInvitations { get; set; }
        public DbSet<Folder> Folders { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<AccessForRegisteredUsers> AccessesForRegisteredUsers{ get; set; }
        public DbSet<AccessForUnregisteredUsers> AccessesForUnregisteredUsers { get; set; }

        public RecoDbContext(DbContextOptions<RecoDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Configure();
        }
    }
}
