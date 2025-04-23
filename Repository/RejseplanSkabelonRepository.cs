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
            return await _context.RejseplanSkabeloner.ToListAsync();
        }
        public async Task<RejseplanSkabelonModel> GetRejseplanSkabelonerIdAsync(int id)
        {
            return await _context.RejseplanSkabeloner.FindAsync(id);
        }
        public async Task<RejseplanSkabelonModel> AddRejseplanSkabelonerAsync(RejseplanSkabelonModel rejseplanSkabelon)
        {
            var result = await _context.RejseplanSkabeloner.AddAsync(rejseplanSkabelon);
            await _context.SaveChangesAsync();
            return result.Entity;
        }
        public async Task<RejseplanSkabelonModel> UpdateRejseplanerAsync(RejseplanSkabelonModel rejseplanSkabelon)
        {
            try
            {
                var result = _context.RejseplanSkabeloner.Update(rejseplanSkabelon);
                await _context.SaveChangesAsync();
                return result.Entity;
            }
            catch (DbUpdateConcurrencyException)
            {
                throw new DbUpdateConcurrencyException("Kunden blev ændret af en anden, genlæs siden o+g prøv igen.");
            }
        }

        public async Task<bool> DeleteRejseplanerSkabelonerAsync(int id)
        {
            var rejseplanSkabelon = await GetRejseplanSkabelonerByIdAsync(id);
            if (rejseplanSkabelon != null)
            {
                _context.RejseplanSkabeloner.Remove(rejseplanSkabelon);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

    }
    
}
