using System.Linq.Expressions;
using TANE.Skabelon.Api.Dtos;
using TANE.Skabelon.Api.Models;

namespace TANE.Skabelon.Api.GenericRepositories
{
        //Copy from Rejseplan
        public interface IGenericRepository<T> where T : BaseEntity
        {
        Task<T?> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        Task DeleteAsync(T entity);
        Task UpdateAsync(T entity);
        Task AddAsync(T entity);
        Task SaveChangesAsync();
        IQueryable<T> Query();

        Task<IReadOnlyList<T>> ListAsync(
            Expression<Func<T, bool>> predicate,
            CancellationToken ct = default);

        Task<IReadOnlyList<TResult>> ListAsync<TResult>(
            Expression<Func<T, bool>> predicate,
            Func<IQueryable<T>, IQueryable<TResult>> projector,
            CancellationToken ct = default);

        Task<T?> GetFirstOrDefaultAsync(
            Expression<Func<T, bool>> predicate,
            params Expression<Func<T, object>>[] includeProperties);

        Task<IEnumerable<T>> GetAllAsync(
            Func<IQueryable<T>, IQueryable<T>>? include = null
        );

        Task<T> GetByIdWithIncludeAsync(int id, params Expression<Func<T, object>>[] includes);

        void Update(T entity);
    }
}

