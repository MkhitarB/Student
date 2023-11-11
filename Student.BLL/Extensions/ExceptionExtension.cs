using Microsoft.EntityFrameworkCore;
using Student.Entity.Entities.BaseEntities;
using Student.Infrastructure.Exceptions;

namespace Student.BLL.Extensions
{
    public static class ExceptionExtension
    {
        public static async Task CheckIfExistQuery<TEntity>(this TEntity entity) where TEntity : IQueryable<BaseEntity>
        {
            if (!await entity.AnyAsync())
                throw new SmartException(EntityNotFoundMessageQuery<TEntity>());
        }

        public static string EntityNotFoundMessageQuery<TEntity>() where TEntity : IQueryable<BaseEntity>
        {
            return typeof(TEntity).FullName + " not found";
        }

        public static async Task CheckIfNotExistQuery<TEntity>(this TEntity entity) where TEntity : IQueryable<BaseEntity>
        {
            if (await entity.AnyAsync())
                throw new SmartException(EntityAlreadyExistMessage<TEntity>());
        }

        public static string EntityAlreadyExistMessage<TEntity>() where TEntity : IQueryable<BaseEntity>
        {
            return typeof(TEntity).Name + " is already exist";
        }
    }
}
