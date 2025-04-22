using Microsoft.EntityFrameworkCore;
using TANE.Skabelon.Api.Models;

namespace TANE.Skabelon.Api.Context
{
    public class SkabelonDbContext : DbContext
    {
        public SkabelonDbContext(DbContextOptions<SkabelonDbContext> options) : base(options) { }
        
        public DbSet<RejseplanSkabelon> RejsePlaner { get; set;  }
        public DbSet<TurSkabelon> TurSkabeloner { get; set; }
        public DbSet<DagSkabelon> DagSkabeloner { get; set; }


        // Configuration of DbContext 
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<RejseplanSkabelon>()
                        .HasMany(r => r.TurSkabeloner)
                        .WithRequired(t => t.RejseplanSkabelon)
                        .HasForeignKey(t => t.RejseplanSkabelonId);

            modelBuilder.Entity<TurSkabelon>()
                        .HasMany(t => t.DagSkabeloner)
                        .WithRequired(d => d.TurSkabelon)
                        .HasForeignKey(t => t.TurSkabelonId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
