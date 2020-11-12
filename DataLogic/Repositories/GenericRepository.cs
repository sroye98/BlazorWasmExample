using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLogic.Extensions;
using DataLogic.DataAccess;
using DataLogic.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataLogic.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class, IEntity
    {
        private readonly AppDbContext _context;

        public GenericRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            try
            {
                await _context.Set<TEntity>().AddAsync(entity);
                await _context.SaveChangesAsync();

                return entity;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> CountAsync(Func<TEntity, bool> whereClause)
        {
            try
            {
                int count = 0;
                IQueryable<TEntity> query = _context.Set<TEntity>();

                count = whereClause != null ?
                    query.AsEnumerable().Where(whereClause).Count() :
                    await query.CountAsync();

                return count;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task DeleteAsync(TEntity entity)
        {
            try
            {
                await DeleteAsync(entity.Id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            try
            {
                TEntity entity = await GetAsync(id);
                if (entity == null)
                {
                    throw new Exception("Entity not found");
                }

                _context.Set<TEntity>().Remove(entity);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Task<IQueryable<TEntity>> GetAsync()
        {
            try
            {
                IQueryable<TEntity> entities = _context.Set<TEntity>().AsNoTracking();

                return Task.FromResult(entities);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<TEntity> GetAsync(
            Guid id,
            string[] projections = null)
        {
            try
            {
                if (projections == null)
                {
                    projections = new string[] { };
                }

                IQueryable<TEntity> query = _context.Set<TEntity>().AsNoTracking();

                foreach (string projection in projections)
                {
                    query = query.Include(projection);
                }

                TEntity item = await query.SingleOrDefaultAsync(m => m.Id == id);

                return item;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Task<List<TEntity>> GetAsync(
            string sortColumn,
            Func<TEntity, bool> whereClause,
            bool descending = true,
            int? pageSize = 25,
            string[] projections = null,
            int? skip = 0)
        {
            try
            {
                if (projections == null)
                {
                    projections = new string[] { };
                }

                IQueryable<TEntity> query = _context.Set<TEntity>().AsNoTracking();

                foreach (string projection in projections)
                {
                    query = query.Include(projection);
                }

                if (whereClause != null)
                {
                    query = query.Where(whereClause).AsQueryable();
                }

                if (!string.IsNullOrEmpty(sortColumn))
                {
                    query = descending ?
                        query.OrderByDescending(m => m.GetPropValue(sortColumn)) :
                        query.OrderBy(m => m.GetPropValue(sortColumn));
                }

                List<TEntity> entities = query.ToList();

                return Task.FromResult(entities);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<TEntity> UpdateAsync(
            Guid id,
            TEntity entity)
        {
            try
            {
                if (id != entity.Id)
                {
                    throw new Exception("Id mismatch");
                }

                TEntity entityToUpdate = await GetAsync(id);
                if (entityToUpdate == null)
                {
                    throw new Exception("Entity not found");
                }

                _context.Set<TEntity>().Update(entity);
                await _context.SaveChangesAsync();

                return entity;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
