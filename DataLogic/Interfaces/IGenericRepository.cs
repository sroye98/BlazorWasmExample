using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataLogic.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : class, IEntity
    {
        Task<TEntity> AddAsync(TEntity entity);

        Task<int> CountAsync(Func<TEntity, bool> whereClause);

        Task DeleteAsync(TEntity entity);

        Task DeleteAsync(Guid id);

        Task<IQueryable<TEntity>> GetAsync();

        Task<TEntity> GetAsync(
            Guid id,
            string[] projections = null);

        Task<IEnumerable<TEntity>> GetAsync(
            string sortColumn,
            Func<TEntity, bool> whereClause,
            bool descending = true,
            int? pageSize = 25,
            string[] projections = null,
            int? skip = 0);

        Task<TEntity> UpdateAsync(
            Guid id,
            TEntity entity);
    }
}
