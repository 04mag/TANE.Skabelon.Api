using TANE.Skabelon.Api.Context;
using TANE.Skabelon.Api.Models;
using Microsoft.EntityFrameworkCore;



namespace TANE.Skabelon.Api.Repositories
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
            return await _context.TurSkabelon.ToListAsync();
        }

        public async Task<TurSkabelonModel> GetTurSkabelonByIdAsync(int id)
        {
            return await _context.TurSkabelon.FindAsync(id);
        }

        public async Task<TurSkabelonModel> AddTurSkabelonAsync(TurSkabelonModel turSkabelon)
        {
            {
                var result = await _context.TurSkabelon.AddAsync(turSkabelon);
                await _context.SaveChangesAsync();
                return result.Entity;
            }
        }

        public async Task<TurSkabelonModel> UpdateTurSkabelonAsync(TurSkabelonModel turSkabelon)
        {
            try
            {
                var result = _context.TurSkabelon.Update(turSkabelon);
                await _context.SaveChangesAsync();
                return result.Entity;
            }
            catch (DbUpdateConcurrencyException)
            {
                throw new DbUpdateConcurrencyException("Kunden blev ændret af en anden bruger");
            }
        }

        public async Task<bool> DeleteTurSkabelonAsync(int id)
        {
            var turSkabelon = await GetTurSkabelonByIdAsync(id);
            if (turSkabelon != null)
            {
                _context.TurSkabelon.Remove(turSkabelon);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}