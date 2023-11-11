using Microsoft.EntityFrameworkCore;
using Student.Entity.Entities.BaseEntities;
using Student.Infrastructure.Exceptions;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Student.DAL.Repository
{
    public class EntityRepository : IEntityRepository
    {
        protected readonly IEntityDbContext _context;

        public EntityRepository(IEntityDbContext context)
        {
            _context = context;
        }


        #region Async

        public async Task<T> CreateAsync<T>(T entity) where T : BaseEntity
        {
            await _context.Set<T>().AddAsync(entity);
            return entity;
        }

        public async Task<bool> RemoveAsync<T>(int id) where T : BaseEntity
        {
            try
            {
                var entityToRemove = await GetByIdAsync<T>(id);
                if (entityToRemove == null)
                    return true;
                entityToRemove.IsDeleted = true;
                _context.Set<T>().Update(entityToRemove);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public async Task<bool> RemoveRangeAsync<T>(IList<int> ids) where T : BaseEntity
        {
            try
            {
                var entityToRemove = await Filter<T>(x => ids.Contains(x.Id)).ToListAsync();
                foreach (var variable in entityToRemove)
                {
                    if (variable == null)
                        continue;
                    variable.IsDeleted = true;
                }
                _context.Set<T>().UpdateRange(entityToRemove);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<T> GetByIdAsync<T>(int id, params Expression<Func<T, object>>[] includeExpression) where T : BaseEntity
        {
            var set = _context.Set<T>().Where(x => x.Id == id);
            if (includeExpression.Any())
                set = includeExpression.Aggregate(set, (current, variable) => current.Include(variable));
            return await set.FirstOrDefaultAsync();
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }

        #endregion


        #region Sync
        public IQueryable<T> FilterAsNoTracking<T>(Expression<Func<T, bool>> query,
               params Expression<Func<T, object>>[] includeExpression) where T : BaseEntity
        {
            if (query == null)
                throw new SmartException("Query is null");
            var set = _context.Set<T>().Where(query).AsNoTracking();
            if (includeExpression.Any())
                set = includeExpression.Aggregate(set, (current, variable) => current.Include(variable));
            return set;
        }

        public IQueryable<T> Filter<T>(Expression<Func<T, bool>> query,
        params Expression<Func<T, object>>[] includeExpression) where T : BaseEntity
        {
            if (query == null)
                throw new SmartException("Query is null");
            var set = _context.Set<T>().Where(query);
            if (includeExpression.Any())
                set = includeExpression.Aggregate(set, (current, variable) => current.Include(variable));
            return set;
        }

        public bool Update<T>(T entity, ExpandoObject properties) where T : BaseEntity
        {
            if (entity == null)
                return false;
            _context.Entry(entity).State = EntityState.Unchanged;
            if (!properties.Any())
                return false;
            foreach (var variable in properties)
            {
                _context.Entry(entity).Property(variable.Key).IsModified = true;
                _context.Entry(entity).Property(variable.Key).CurrentValue = variable.Value;
            }
            return true;
        }
        #endregion
        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }

        public IEntityDbContext GetContext()
        {
            return _context;
        }
    }
}
