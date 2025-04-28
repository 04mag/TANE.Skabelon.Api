using TANE.Skabelon.Api.Context;
using TANE.Skabelon.Api.Models;
using Microsoft.EntityFrameworkCore;


namespace TANE.Skabelon.Api.Repository
{
    public class RejseplanSkabelonRepository : IRejseplanSkabelonRepository
    {
        private readonly SkabelonDbContext _context;
        public RejseplanSkabelonRepository(SkabelonDbContext context)
        {
            _context = context;
        }

        public async Task<List<RejseplanSkabelonModel>> GetAllRejseplanSkabelonerAsync()
        {
            return await _context.RejseplanSkabelon.ToListAsync();
        }
        public async Task<RejseplanSkabelonModel> GetRejseplanSkabelonByIdAsync(int id)
        {
            return await _context.RejseplanSkabelon.FindAsync(id);
        }
        public async Task<RejseplanSkabelonModel> AddRejseplanSkabelonAsync(RejseplanSkabelonModel rejseplanSkabelon)
        {
            var result = await _context.RejseplanSkabelon.AddAsync(rejseplanSkabelon);
            await _context.SaveChangesAsync();
            return result.Entity;
        }
        public async Task<RejseplanSkabelonModel> UpdateRejseplanSkabelonAsync(RejseplanSkabelonModel rejseplanSkabelon)
        {
            try
            {
                var result = _context.RejseplanSkabelon.Update(rejseplanSkabelon);
                await _context.SaveChangesAsync();
                return result.Entity;
            }
            catch (DbUpdateConcurrencyException)
            {
                throw new DbUpdateConcurrencyException("Kunden blev ændret af en anden bruger");
            }
        }

        public async Task<bool> DeleteRejseplanSkabelonAsync(int id)
        {
            var rejseplanSkabelon = await GetRejseplanSkabelonByIdAsync(id);
            if (rejseplanSkabelon != null)
            {
                _context.RejseplanSkabelon.Remove(rejseplanSkabelon);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

    }
    
}
