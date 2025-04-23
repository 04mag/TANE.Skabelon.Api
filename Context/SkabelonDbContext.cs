using Microsoft.EntityFrameworkCore;
using TANE.Skabelon.Api.Models;

namespace TANE.Skabelon.Api.Context
{
    public class SkabelonDbContext : DbContext
    {
        public SkabelonDbContext(DbContextOptions<SkabelonDbContext> options) : base(options) { }
        
        public DbSet<RejseplanSkabelonModel> RejseplanSkabeloner { get; set;  }
        public DbSet<TurSkabelonModel> TurSkabeloner { get; set; }
        public DbSet<DagSkabelonModel> DagSkabeloner { get; set; }


        // Configuration of DbContext 
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<RejseplanSkabelonModel>()
                        .HasMany(r => r.TurSkabeloner)
                        .WithRequired(t => t.RejseplanSkabelon)
                        .HasForeignKey(t => t.RejseplanSkabelonId);

            modelBuilder.Entity<TurSkabelonModel>()
                        .HasMany(t => t.DagSkabeloner)
                        .WithRequired(d => d.TurSkabelon)
                        .HasForeignKey(t => t.TurSkabelonId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
