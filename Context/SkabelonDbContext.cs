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
       
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=LocalHost;Database=TANE_Skabelon_Db;Trusted_Connection=True;Trust Server Certificate=True");
        }
    }
}
