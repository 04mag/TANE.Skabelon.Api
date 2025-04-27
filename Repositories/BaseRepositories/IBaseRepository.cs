using TANE.Skabelon.Api.Models;

namespace TANE.Skabelon.Api.Repositories.BaseRepositories
{
    public interface IBaseRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task<T> AddAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task DeleteAsync(int id);
        Task SaveChangesAsync(); 
    }
}

