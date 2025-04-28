using TANE.Skabelon.Api.Models;

namespace TANE.Skabelon.Api.Repository
{
    public interface IRejseplanSkabelonRepository
    {
            Task<List<RejseplanSkabelonModel>> GetAllRejseplanSkabelonerAsync();
            Task<RejseplanSkabelonModel> GetRejseplanSkabelonByIdAsync(int id);
            Task<RejseplanSkabelonModel> AddRejseplanSkabelonAsync(RejseplanSkabelonModel rejseplanSkabelon);
            Task<RejseplanSkabelonModel> UpdateRejseplanSkabelonAsync(RejseplanSkabelonModel rejseplanSkabelon);
            Task<bool> DeleteRejseplanSkabelonAsync(int id);
        
    }
}
