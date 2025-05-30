﻿using Microsoft.EntityFrameworkCore;
using TANE.Skabelon.Api.Models;

namespace TANE.Skabelon.Api.Context
{
    public class SkabelonDbContext : DbContext
    {
        public SkabelonDbContext(DbContextOptions<SkabelonDbContext> options) : base(options) { }

        public DbSet<RejseplanSkabelonModel> RejseplanSkabelon { get; set; }
        public DbSet<DagSkabelonModel> DagSkabelon { get; set; }
        public DbSet<TurSkabelonModel> TurSkabelon { get; set; }
        public DbSet<DagTurSkabelon> DagTurSkabelon { get; set; }
        public DbSet<RejseplanTurSkabelon> RejseplanTurSkabelon { get; set; }


        // Pseudocode plan:
        // 1. Identify all entities with a RowVersion property (from BaseEntity or directly defined).
        // 2. In OnModelCreating, configure the RowVersion property as a concurrency token and as a row version (timestamp).
        // 3. Use modelBuilder.Entity<T>().Property(e => e.RowVersion).IsRowVersion(); for each entity with RowVersion.

        // Implementation:
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure RowVersion as concurrency token for all entities inheriting BaseEntity
            modelBuilder.Entity<DagSkabelonModel>()
                .Property(e => e.RowVersion)
                .IsRowVersion();

            modelBuilder.Entity<TurSkabelonModel>()
                .Property(e => e.RowVersion)
                .IsRowVersion();

            modelBuilder.Entity<RejseplanSkabelonModel>()
                .Property(e => e.RowVersion)
                .IsRowVersion();

            // Existing configuration...
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
               },
               new DagTurSkabelon
               {
                   DagSkabelonId = 1,
                   TurSkabelonId = 2,
                   Order = 1
               }
            );

            modelBuilder.Entity<RejseplanSkabelonModel>().HasData(
               new RejseplanSkabelonModel
               {
                   Id = 1,
                   Titel = "Rejseplan 1",
                   Beskrivelse = "Beskrivelse for Rejseplan 1"
               },
               new RejseplanSkabelonModel
               {
                   Id = 2,
                   Titel = "Rejseplan 2",
                   Beskrivelse = "Beskrivelse for Rejseplan 2"
               }
            );

            modelBuilder.Entity<RejseplanTurSkabelon>().HasData(
               new RejseplanTurSkabelon
               {
                   RejseplanSkabelonId = 1,
                   TurSkabelonId = 1,
                   Order = 1
               },
               new RejseplanTurSkabelon
               {
                   RejseplanSkabelonId = 2,
                   TurSkabelonId = 2,
                   Order = 1
               },
               new RejseplanTurSkabelon
               {
                   RejseplanSkabelonId = 2,
                   TurSkabelonId = 1,
                   Order = 2
               }
            );
        }
    }
}   
