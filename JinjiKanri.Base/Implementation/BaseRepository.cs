using System;
using System.Collections.Generic;
using System.Text;
using JinjiKanri.Base.Interface;
using Microsoft.EntityFrameworkCore;

namespace JinjiKanri.Base.Implementation
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private DbContext _dbContext;
        private DbSet<T> _dbSet;

        public BaseRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<T>();
        }

        public async Task<IEnumerable<T?>> GetAll()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T?> GetById(long id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task InsertAsync(T entity)
        {
            if(entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            else
            {
                await _dbSet.AddAsync(entity);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task UpdateAsync(T existingEntity, T updatedEntity)
        {
            if (existingEntity == null) 
            {
                throw new ArgumentNullException(nameof(existingEntity));
            }
            if (updatedEntity == null)
            {
                throw new ArgumentNullException(nameof(updatedEntity));
            }

            _dbContext.Entry(existingEntity).CurrentValues.SetValues(updatedEntity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            _dbSet.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IBaseRepository<T>> Where(Func<T, bool> predicate)
        {
            _dbSet.Where(predicate);
            return this;
        }

        public async Task<IEnumerable<T>> Limit(int limit)
        {
            return await _dbSet.Take(limit).ToListAsync();
        }
    }
}
