using Student.Entity.Entities.BaseEntities;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Student.DAL.Repository
{
    public interface IEntityRepository : IDisposable
    {
        Task<T> CreateAsync<T>(T entity) where T : BaseEntity;

        IQueryable<T> FilterAsNoTracking<T>(Expression<Func<T, bool>> query,
               params Expression<Func<T, object>>[] includeExpression) where T : BaseEntity;
        Task<bool> RemoveAsync<T>(int id) where T : BaseEntity;
        Task<bool> RemoveRangeAsync<T>(IList<int> ids) where T : BaseEntity;
        Task<T> GetByIdAsync<T>(int id, params Expression<Func<T, object>>[] includeExpression) where T : BaseEntity;
        IQueryable<T> Filter<T>(Expression<Func<T, bool>> query,
        params Expression<Func<T, object>>[] includeExpression) where T : BaseEntity;

        bool Update<T>(T entity, ExpandoObject properties) where T : BaseEntity;
 
         Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        IEntityDbContext GetContext();
    }
}
