using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using TANE.Skabelon.Api.Models;
using TANE.Skabelon.Api.Context;

namespace TANE.Skabelon.Api.Repositories.BaseRepositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected readonly SkabelonDbContext _context;
        protected readonly DbSet<T> _dbSet;


        public BaseRepository(SkabelonDbContext context)
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

        public async Task DeleteAsync(int id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity == null) return; 
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }
        
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<T> UpdateAsync (T entity)
        {
            try
            {
                _context.Entry(entity).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return entity;
            }
            catch (DbUpdateConcurrencyException)
            {
                throw new InvalidOperationException("Concurrence conflict");
            }
        }
    }
}
