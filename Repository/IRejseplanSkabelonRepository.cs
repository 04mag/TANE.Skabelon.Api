using TANE.Skabelon.Api.Models;

namespace TANE.Skabelon.Api.Repository
{
    public interface IRejseplanSkabelonRepository
    {
            Task<List<RejseplanSkabelonModel>> GetAllRejseplanSkabelonerAsync();
            Task<RejseplanSkabelonModel> GetRejseplanSkabelonerByIdAsync(int id);
            Task AddRejseplanSkabelonerAsync(RejseplanSkabelonModel rejseplanSkabelonModel);
            Task UpdateRejseplanSkabelonerAsync(RejseplanSkabelonModel rejseplanSkabelonModel);
            Task DeleteRejseplanSkabelonAsync(int id);
        
    }
}
