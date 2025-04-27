using TANE.Skabelon.Api.Models;


namespace TANE.Skabelon.Api.Repository
{
    public interface IDagSkabelonRepository
    {
        Task<List<DagSkabelonModel>> GetAllDagSkabelonerAsync();
        Task<DagSkabelonModel> DagSkabelonerByIdAsync(int id);
        Task AddDagSkabelonerAsync(DagSkabelonModel dagSkabelonModel);
        Task UpdateDagSkabelonerAsync(DagSkabelonModel dagSkabelonModel);
        Task DeleteDagSkabelonAsync(int id);

    }
}
