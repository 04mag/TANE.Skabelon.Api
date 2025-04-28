using TANE.Skabelon.Api.Models;


namespace TANE.Skabelon.Api.Repositories
{
    public interface IDagSkabelonRepository
    {
        Task<List<DagSkabelonModel>> GetAllDagSkabelonerAsync();
        Task<DagSkabelonModel> GetDagSkabelonByIdAsync(int id);
        Task<DagSkabelonModel> AddDagSkabelonAsync(DagSkabelonModel dagSkabelon);
        Task<DagSkabelonModel> UpdateDagSkabelonAsync(DagSkabelonModel dagSkabelon);
        Task<bool> DeleteDagSkabelonAsync(int id);

    }
}
