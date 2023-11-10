using Student.Entity.Entities.BaseEntities;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync(cancellationToken);
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
