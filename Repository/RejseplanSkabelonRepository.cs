using TANE.Skabelon.Api.Context;
using TANE.Skabelon.Api.Models;
using TANE.Skabelon.Api.Repository;


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
        public async Task AddRejseplanSkabelonerAsync(RejseplanSkabelonModel rejseplanSkabelon)
        {
            await _context.RejseplanSkabeloner.AddAsync(rejseplanSkabelon);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateRejseplanerAsync(RejseplanSkabelonModel rejseplanSkabelon)
        {
            try
            {
                _context.RejseplanSkabeloner.Update(rejseplanSkabelon);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw new DbUpdateConcurrencyException("Kunden blev ændret af en anden, genlæs siden og prøv igen.");
            }
        }

        public async Task DeleteRejseplanerSkabelonerAsync(int id)
        {
            var rejseplanSkabelon = await GetRejseplanSkabelonerByIdAsync(id);
            if (rejseplanSkabelon != null)
            {
                _context.RejseplanSkabeloner.Remove(rejseplanSkabelon);
                await _context.SaveChangesAsync();
            }
        }

    }
    
}
