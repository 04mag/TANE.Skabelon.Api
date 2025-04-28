using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using TANE.Skabelon.Api.Models;
using TANE.Skabelon.Api.Context;

namespace TANE.Skabelon.Api.GenericRepositories
{
    //Copy from Rejseplan
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        protected readonly SkabelonDbContext _context;
        protected readonly DbSet<T> _dbSet;


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
            try
            {
                _dbSet.Remove(entity);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw new Exception($"Entitet {typeof(T).Name} blev opdateret af en anden bruger.");
            }
        }
        
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            try
            {
                _dbSet.Attach(entity).State = EntityState.Modified;
                _context.Entry(entity).Property(e => e.RowVersion).OriginalValue = entity.RowVersion; // Sætter original rowversion til den nuværende rowversion.
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw new Exception($"The entity {typeof(T).Name} was modified by another user.");
            }
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
        public IQueryable<T> Query()
        {
            return _dbSet.AsQueryable();
        }
    }
    
}
