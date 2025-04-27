using TANE.Skabelon.Api.Context;
using TANE.Skabelon.Api.Models;
using Microsoft.EntityFrameworkCore;



namespace TANE.Skabelon.Api.Repository
{
    public class TurSkabelonRepository : ITurSkabelonRepository
    {
        private readonly SkabelonDbContext _context;

        public TurSkabelonRepository(SkabelonDbContext context)
        {
            _context = context;
        }

        public async Task<List<TurSkabelonModel>> GetAllTurSkabelonerAsync()
        {
            return await _context.TurSkabeloner.ToListAsync();
        }

        public async Task<TurSkabelonModel> GetTurSkabelonerIdAsync(int id)
        {
            return await _context.TurSkabeloner.FindAsync(id);
        }

        public async Task<TurSkabelonModel> AddTurSkabelonerAsync(TurSkabelonModel turSkabelonModel)
        {
            {
                var result = await _context.TurSkabeloner.AddAsync(turSkabelonModel);
                await _context.SaveChangesAsync();
                return result.Entity;
            }
        }

        public async Task<TurSkabelonModel> UpdateTurskabelonerAsync(TurSkabelonModel turSkabelon)
        {
            try
            {
                var result = _context.TurSkabeloner.Update(turSkabelon);
                await _context.SaveChangesAsync();
                return result.Entity;
            }
            catch (DbUpdateConcurrencyException)
            {
                throw new DbUpdateConcurrencyException("Kunden blev ændret af en anden bruger");
            }
        }

        public async Task<bool> DeleteTurSkabelonerAsync(int id)
        {
            var turSkabelon = await GetTurSkabelonerIdAsync(id);
            if (turSkabelon != null)
            {
                _context.TurSkabeloner.Remove(turSkabelon);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}