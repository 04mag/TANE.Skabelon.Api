using TANE.Skabelon.Api.Dtos;
using TANE.Skabelon.Api.Models;

namespace TANE.Skabelon.Api.GenericRepositories
{
        //Copy from Rejseplan
        public interface IGenericRepository<T> where T : BaseEntity
        {
            Task<T?> GetByIdAsync(int id);
            Task<IEnumerable<T>> GetAllAsync(int turSkabelonId);
            Task DeleteAsync(T entity);
            Task UpdateAsync(T entity);
            Task AddAsync(T entity);
            Task SaveChangesAsync();
            IQueryable<T> Query();
        }
}

