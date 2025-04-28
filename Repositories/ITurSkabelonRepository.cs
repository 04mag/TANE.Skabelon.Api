using TANE.Skabelon.Api.Models;

namespace TANE.Skabelon.Api.Repositories
{
    public interface ITurSkabelonRepository
    {
        Task<List<TurSkabelonModel>> GetAllTurSkabelonerAsync();
        Task<TurSkabelonModel> GetTurSkabelonByIdAsync(int id);
        Task<TurSkabelonModel> AddTurSkabelonAsync(TurSkabelonModel turSkabelon);
        Task<TurSkabelonModel> UpdateTurSkabelonAsync(TurSkabelonModel turSkabelon);
        Task<bool> DeleteTurSkabelonAsync(int id);

    }
}
