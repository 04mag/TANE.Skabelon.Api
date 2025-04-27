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
            return await _context.DagSkabeloner.ToListAsync();
        }

        public async Task<TurSkabelonModel> GetDagSkabelonerIdAsync(int id)
        {
            return await _context.DagSkabeloner.FindAsync(id);
        }

        public async Task <TurSkabelonModel> AddDagSkabelonerAsync(TurSkabelonModel dagSkabelonModel)
        {
            {
                var result = await _context.DagSkabeloner.AddAsync(dagSkabelonModel);
                await _context.SaveChangesAsync();
                return result.Entity;
            }
        }

        public async Task<TurSkabelonModel> UpdateDagskabelonerAsync(TurSkabelonModel dagSkabelon)
        {
            try
            {
                var result = _context.DagSkabeloner.Update(dagSkabelon);
                await _context.SaveChangesAsync();
                return result.Entity;
            }
            catch (DbUpdateConcurrencyException)
            {
                throw new DbUpdateConcurrencyException("Kunden blev ændret af en anden bruger");
            }
        }

        public async Task<bool> DeleteDagSkabelonerAsync(int id)
        {
            var dagSkabelon = await GetDagSkabelonerIdAsync(id);
            if (dagSkabelon != null)
            {
                _context.DagSkabeloner.Remove(dagSkabelon);
                await _context.SaveChangesAsync();
                return true;
            }
            return false; 
        }
    }
}
