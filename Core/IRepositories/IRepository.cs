using Core.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IRepository<TEntity> where TEntity : BaseEntity
    {
        Task AddAsync(TEntity entity);
        Task DeleteAsync(Guid id);
        Task RemoveFromDbAsync(Guid id);
        Task UpdateAsync(TEntity entity);
        Task<List<TEntity>> GetAllAsync();
        Task<List<TEntity>> GetAllInActiveAsync();
        Task<List<TEntity>> GetAllActiveAsync();
        Task<TEntity> GetbyIdAsync(Guid id);
        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> filter);
        Task<List<TEntity>> GetbyFilterAsync(Expression<Func<TEntity, bool>> filter);
    }
}
