using Microsoft.EntityFrameworkCore;
using TANE.Skabelon.Api.Context;
using TANE.Skabelon.Api.Models;



namespace TANE.Skabelon.Api.Repositories
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
            return await _context.DagSkabelon.ToListAsync();
        }

        public async Task<DagSkabelonModel> GetDagSkabelonByIdAsync(int id)
        {
            return await _context.DagSkabelon.FindAsync(id);
        }

        public async Task<DagSkabelonModel> AddDagSkabelonAsync(DagSkabelonModel dagSkabelon)
        {
            {
                var result = await _context.DagSkabelon.AddAsync(dagSkabelon);
                await _context.SaveChangesAsync();
                return result.Entity;
            }
        }

        public async Task<DagSkabelonModel> UpdateDagSkabelonAsync(DagSkabelonModel dagSkabelon)
        {
            try
            {
                var result = _context.DagSkabelon.Update(dagSkabelon);
                await _context.SaveChangesAsync();
                return result.Entity;
            }
            catch (DbUpdateConcurrencyException)
            {
                throw new DbUpdateConcurrencyException("Kunden blev ændret af en anden bruger");
            }
        }

        public async Task<bool> DeleteDagSkabelonAsync(int id)
        {
            var dagSkabelon = await GetDagSkabelonByIdAsync(id);
            if (dagSkabelon != null)
            {
                _context.DagSkabelon.Remove(dagSkabelon);
                await _context.SaveChangesAsync();
                return true;
            }
            return false; 
        }
    }
}
