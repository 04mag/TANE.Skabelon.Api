using Microsoft.EntityFrameworkCore;
using TANE.Skabelon.Api.Models;

namespace TANE.Skabelon.Api.Context
{
    public class SkabelonDbContext : DbContext
    {
        public SkabelonDbContext(DbContextOptions<SkabelonDbContext> options) : base(options) { }

        public DbSet<RejseplanSkabelonModel> RejseplanSkabelon { get; set; }
        public DbSet<TurSkabelonModel> TurSkabelon { get; set; }
        public DbSet<DagSkabelonModel> DagSkabelon { get; set; }
        public DbSet<DagTurSkabelon> DagTurSkabelon { get; set; }
        public DbSet<RejseplanTurSkabelon> RejseplanTurSkabelon { get; set; }


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

            // Seed test data  
            modelBuilder.Entity<DagSkabelonModel>().HasData(
               new DagSkabelonModel
               {
                   Id = 1,
                   Titel = "Dag 1",
                   Beskrivelse = "Beskrivelse for Dag 1",
                   Aktiviteter = "Aktiviteter for Dag 1",
                   Måltider = "Måltider for Dag 1",
                   Overnatning = "Overnatning for Dag 1"
               },
               new DagSkabelonModel
               {
                   Id = 2,
                   Titel = "Dag 2",
                   Beskrivelse = "Beskrivelse for Dag 2",
                   Aktiviteter = "Aktiviteter for Dag 2",
                   Måltider = "Måltider for Dag 2",
                   Overnatning = "Overnatning for Dag 2"
               }
            );

            modelBuilder.Entity<TurSkabelonModel>().HasData(
               new TurSkabelonModel
               {
                   Id = 1,
                   Titel = "Tur 1",
                   Beskrivelse = "Beskrivelse for Tur 1",
                   Pris = 100.0
               },
               new TurSkabelonModel
               {
                   Id = 2,
                   Titel = "Tur 2",
                   Beskrivelse = "Beskrivelse for Tur 2",
                   Pris = 200.0
               }
            );

            modelBuilder.Entity<DagTurSkabelon>().HasData(
               new DagTurSkabelon
               {
                   DagSkabelonId = 1,
                   TurSkabelonId = 1,
                   Order = 1
               },
               new DagTurSkabelon
               {
                   DagSkabelonId = 2,
                   TurSkabelonId = 2,
                   Order = 2
               }
            );
        }
    }
}   
