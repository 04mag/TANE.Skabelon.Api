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
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=LocalHost;Database=TANE_Skabelon_Db;Trusted_Connection=True;Trust Server Certificate=True");
        }
    }
}
