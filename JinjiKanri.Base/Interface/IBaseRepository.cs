using System;
using System.Collections.Generic;
using System.Text;

namespace JinjiKanri.Base.Interface
{
    public interface IBaseRepository<T> where T : class
    {
        Task<IEnumerable<T?>> GetAll();
        Task<T?> GetById(long id);

        Task InsertAsync(T entity);

        Task UpdateAsync(T existingEntity, T updatedEntity);

        Task DeleteAsync(T entity);

    }
}
