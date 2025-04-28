using Microsoft.EntityFrameworkCore;
using TANE.Skabelon.Api.Models;

namespace TANE.Skabelon.Api.Context
{
    public class SkabelonDbContext : DbContext
    {
        public SkabelonDbContext(DbContextOptions<SkabelonDbContext> options) : base(options) { }
        
        public DbSet<RejseplanSkabelonModel> RejseplanSkabelon { get; set;  }
        public DbSet<TurSkabelonModel> TurSkabelon { get; set; }
        public DbSet<DagSkabelonModel> DagSkabelon { get; set; }


        // Configuration of DbContext 
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<RejseplanSkabelonModel>()
                .HasMany(r => r.TurSkabeloner)
                .WithOne(t => t.RejseplanSkabelon) 
                .HasForeignKey(t => t.RejseplanSkabelonId)
                .IsRequired();

           
            modelBuilder.Entity<DagSkabelonModel>()
                .HasOne(d => d.TurSkabelon)
                .WithMany(t => t.DagSkabeloner) 
                .HasForeignKey(d => d.TurSkabelonId)
                .IsRequired();

            // Concurrency-token som rowversion
            modelBuilder.Entity<RejseplanSkabelonModel>()
                .Property(r => r.RowVersion)
                .IsRowVersion();

            modelBuilder.Entity<TurSkabelonModel>()
                .Property(t => t.RowVersion)
                .IsRowVersion();

            modelBuilder.Entity<DagSkabelonModel>()
                .Property(d => d.RowVersion)
                .IsRowVersion();
        }
    }
}
