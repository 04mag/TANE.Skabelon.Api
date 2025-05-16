using Microsoft.EntityFrameworkCore;
using TANE.Skabelon.Api.Models;

namespace TANE.Skabelon.Api.Context
{
    public class SkabelonDbContext : DbContext
    {
        public SkabelonDbContext(DbContextOptions<SkabelonDbContext> options) : base(options) { }

        public DbSet<RejseplanSkabelonModel> RejseplanSkabelon { get; set; }
        public DbSet<TurSkabelonModel> DagSkabelon { get; set; }
        public DbSet<DagSkabelonModel> DagSkabelon { get; set; }


        // Delete dagskabelon, hvis den fjernes fra turskabelon
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DagTurSkabelon>()
                .HasKey(ps => new { ps.DagSkabelonId, ps.TurSkabelonId });

            modelBuilder.Entity<DagTurSkabelon>()
                .HasOne(ps => ps.DagSkabelon)
                .WithMany(p => p.DagTurSkabelon)
                .HasForeignKey(ps => ps.DagSkabelonId);

            modelBuilder.Entity<DagTurSkabelon>()
                .HasOne(ps => ps.TurSkabelon)
                .WithMany(s => s.DagTurSkabelon)
                .HasForeignKey(ps => ps.TurSkabelonId);

            modelBuilder.Entity<RejseplanTurSkabelon>()
                .HasKey(ps => new { ps.RejseplanSkabelonId, ps.TurSkabelonId });

            modelBuilder.Entity<RejseplanTurSkabelon>()
                .HasOne(ps => ps.RejseplanSkabelon)
                .WithMany(p => p.RejseplanTurSkabelon)
                .HasForeignKey(ps => ps.RejseplanSkabelonId);

            modelBuilder.Entity<RejseplanTurSkabelon>()
                .HasOne(ps => ps.TurSkabelon)
                .WithMany(s => s.RejseplanTurSkabelon)
                .HasForeignKey(ps => ps.TurSkabelonId);
        }
    }
}   
