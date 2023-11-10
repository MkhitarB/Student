using Student.Entity.Entities.BaseEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Student.DAL.Repository
{
    public interface IEntityRepository : IDisposable
    {
        Task<T> CreateAsync<T>(T entity) where T : BaseEntity;

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
