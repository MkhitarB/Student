using Microsoft.EntityFrameworkCore;
using Student.Entity.Entities;
using Student.Entity.Entities.BaseEntities;

namespace Student.DAL
{
    public interface IEntityDbContext : IDisposable
    {
        DbSet<User> Users { get; set; }

        DbSet<TEntity> Set<TEntity>() where TEntity : BaseEntity;
        Task<int> SaveChangesAsync(CancellationToken token = default);
    }
}
