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


        // Delete dagskabelon, hvis den fjernes fra turskabelon
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TurSkabelonModel>()
                .HasMany(t => t.Dage)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);
            
        }
    }
}
