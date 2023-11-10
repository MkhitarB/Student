using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Student.Entity.Entities;
using Student.Entity.Entities.BaseEntities;
using Student.Infrastructure.NewFolder;
using System.Reflection;

namespace Student.DAL
{
    public class EntityDbContext : DbContext, IEntityDbContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public EntityDbContext(DbContextOptions<EntityDbContext> options) : base(options)
        {
            _httpContextAccessor = new HttpContextAccessor();
        }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var assembly = Assembly.Load("Student.Entity");
            modelBuilder.ApplyConfigurationsFromAssembly(assembly);
        }

        #region Db Functions
        public new DbSet<TEntity> Set<TEntity>() where TEntity : BaseEntity
        {
            return base.Set<TEntity>();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken token = default)
        {
            try
            {
                AddTimestamps(_httpContextAccessor);
                return await base.SaveChangesAsync(token);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private void AddTimestamps(IHttpContextAccessor httpContextAccessor)
        {
            var currentUserId = 0;
            var entities = ChangeTracker.Entries().Where(x => x.Entity is IBaseEntity && (x.State == EntityState.Added || x.State == EntityState.Modified));
            if (_httpContextAccessor.HttpContext != null)
                int.TryParse(httpContextAccessor.HttpContext.User?.Claims?.Where(c => c.Type == "personId")
                    .Select(c => c.Value).FirstOrDefault(), out currentUserId);

            foreach (var entity in entities)
            {
                if (entity.State == EntityState.Added)
                {
                    ((IBaseEntity)entity.Entity).CreatedDt = DateTime.UtcNow;
                    ((IBaseEntity)entity.Entity).CreatedBy = currentUserId;
                }

                ((IBaseEntity)entity.Entity).UpdatedDt = DateTime.UtcNow;
                ((IBaseEntity)entity.Entity).UpdatedBy = currentUserId;
            }
        }
        #endregion

        public class EntityDbContextFactory : IDesignTimeDbContextFactory<EntityDbContext>
        {
            public EntityDbContext CreateDbContext(string[] args)
            {
                var optionsBuilder = new DbContextOptionsBuilder<EntityDbContext>();
                optionsBuilder.EnableSensitiveDataLogging();
                var connectionString = ConstValues.ConnectionString;
                optionsBuilder.UseSqlServer(connectionString);

                return new EntityDbContext(optionsBuilder.Options);
            }
        }
    }
}