using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using TANE.Skabelon.Api.Models;
using TANE.Skabelon.Api.Context;
using System.Linq.Expressions;

namespace TANE.Skabelon.Api.GenericRepositories
{
    //Copy from Rejseplan
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        protected readonly SkabelonDbContext _context;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(SkabelonDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {

            // Attach entity and set original RowVersion to detect conflicts
            _dbSet.Attach(entity);
            var entry = _context.Entry(entity);
            entry.Property(e => e.RowVersion).OriginalValue = entity.RowVersion;

            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {

            _dbSet.Attach(entity);
            var entry = _context.Entry(entity);

            // 2) Mark *all* properties as modified…
            // entry.State = EntityState.Modified;

            // 3) …but then clear out the key(s) so EF won’t try to SET them
            var keyNames = _context.Model
                .FindEntityType(typeof(T))
                .FindPrimaryKey()
                .Properties
                .Select(pk => pk.Name);

            foreach (var key in keyNames)
            {
                entry.Property(key).IsModified = false;
            }

            // 4) Concurrency: tell EF what the original RowVersion was…
            entry.Property(e => e.RowVersion)
                .OriginalValue = entity.RowVersion;
            // …and don’t update it in the SET clause
            entry.Property(e => e.RowVersion).IsModified = false;

            await _context.SaveChangesAsync();


        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public IQueryable<T> Query() =>
            _context.Set<T>().AsNoTracking();

        public async Task<IReadOnlyList<T>> ListAsync(
            Expression<Func<T, bool>> predicate,
            CancellationToken ct = default)
        {
            return await Query()
                .Where(predicate)
                .ToListAsync(ct);
        }

        public async Task<IReadOnlyList<TResult>> ListAsync<TResult>(
            Expression<Func<T, bool>> predicate,
            Func<IQueryable<T>, IQueryable<TResult>> projector,
            CancellationToken ct = default)
        {
            // Her ligger EF-detaljerne og AutoMapper‐udvidelsen
            var q = Query().Where(predicate);
            var projected = projector(q);
            return await projected.ToListAsync(ct);
        }

        public async Task<T?> GetFirstOrDefaultAsync(
            Expression<Func<T, bool>> predicate,
            params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _context.Set<T>();
            foreach (var include in includeProperties)
            {
                query = query.Include(include);
            }
            return await query.FirstOrDefaultAsync(predicate);
        }

        public async Task<IEnumerable<T>> GetAllAsync(
            Func<IQueryable<T>, IQueryable<T>>? include = null)
        {
            IQueryable<T> query = _dbSet;
            if (include != null)
                query = include(query);
            return await query.ToListAsync();
        }

        public async Task<T> GetByIdWithIncludeAsync(int id, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _dbSet;
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
            // Antager entiteten har en int 'Id'-property
            return await query.FirstOrDefaultAsync(e => EF.Property<int>(e, "Id") == id);
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }
    }
}