using TANE.Skabelon.Api.Models;

namespace TANE.Skabelon.Api.Repository
{
    public interface ITurSkabelonRepository
    {
        Task<List<TurSkabelonModel>> GetAllTurSkabelonerAsync();
        Task<TurSkabelonModel> TurSkabelonerByIdAsync(int id);
        Task AddTurSkabelonerAsync(TurSkabelonModel turSkabelonModel);
        Task UpdateTurSkabelonerAsync(TurSkabelonModel turSkabelonModel);
        Task DeleteTurSkabelonAsync(int id);

    }
}
