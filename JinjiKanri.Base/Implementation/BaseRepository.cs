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
        public async Task<T?> GetById(string id)
        {
            return await _dbSet.FindAsync(id);
        }
    }
}
