using TANE.Skabelon.Api.Context;
using TANE.Skabelon.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace TANE.Skabelon.Api.Repository
{
    public class DagSkabelonRepository : IDagSkabelonRepository 
    {
        private readonly SkabelonDbContext _context;

        public DagSkabelonRepository(SkabelonDbContext context)
        {
            _context = context;
        }

        public async Task<List<DagSkabelonModel>> GetAllDagSkabelonerAsync()
        {
            return await _context.DagSkabeloner.ToListAsync();
        }

        public async Task<DagSkabelonModel> GetDagSkabelonerIdAsync(int id)
        {
            return await _context.DagSkabeloner.FindAsync(id);
        }

        public async Task AddDagSka
    }
}
