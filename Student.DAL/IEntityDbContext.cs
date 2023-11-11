using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Student.Entity.Entities;
using Student.Entity.Entities.BaseEntities;

namespace Student.DAL
{
    public interface IEntityDbContext : IDisposable
    {
        DbSet<User> Users { get; set; }
        DbSet<Class> Classes { get; set; }
        DbSet<Course> Courses { get; set; }

        DbSet<TEntity> Set<TEntity>() where TEntity : BaseEntity;
        Task<int> SaveChangesAsync(CancellationToken token = default);

        DatabaseFacade Database { get; }
        EntityEntry Entry(object entity);
    }
}
