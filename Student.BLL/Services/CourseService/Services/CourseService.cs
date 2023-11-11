using Microsoft.EntityFrameworkCore;
using Student.BLL.Extensions;
using Student.BLL.Mediator;
using Student.BLL.Services.CourseService.Services.Commands.Create;
using Student.BLL.Services.CourseService.Services.Commands.Delete;
using Student.BLL.Services.CourseService.Services.Queries;
using Student.BLL.Services.UserService.Services.Commands.Create;
using Student.DAL.Repository;
using Student.DTO.ViewModels.Courses.ResModels;
using Student.Entity.Entities;
using Student.Infrastructure.Enums.EntityEnums;
using Student.Infrastructure.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Student.BLL.Services.CourseService.Services
{
    public class CourseService : ICourseService
    {
        private readonly IEntityRepository _repository;
        private readonly IMediatorHandler _mediator;
        public CourseService(IEntityRepository repository, IMediatorHandler mediator)
        {
            _repository = repository;
            _mediator = mediator;
        }

        public async Task<bool> AssignAsync(int classId, int userId)
        {
            var user = _repository.FilterAsNoTracking<User>(u => u.Id == userId && u.Type == UserType.Student);
            await user.CheckIfExistQuery();

            var classEntity = _repository.FilterAsNoTracking<Class>(c => c.Id == classId);
            await classEntity.CheckIfExistQuery();

            bool isAlreadyAssigned = await _repository.FilterAsNoTracking<Course>(c => c.UserId == userId && c.ClassId == classId).AnyAsync();

            if (!isAlreadyAssigned)
            {
                using (var transaction = _repository.GetContext().Database.BeginTransaction())
                {
                    try
                    {
                        await _mediator.Send(new AssignACourseCommand(classId, userId));

                        transaction.Commit();

                        return true;
                    }
                    catch (Exception e)
                    {
                        transaction.Rollback();
                        throw new Exception(e.Message, e.InnerException);
                    }
                }
                
            }
            else
            {
                throw new SmartException("Already Assigned");
            }
        }
        public async Task<List<GetUserWithAssignedClassesResModel>> GetUserAsignedClassesAsync(int userId)
        {
            var user = _repository.FilterAsNoTracking<User>(u => u.Id == userId && u.Type == UserType.Student);
            await user.CheckIfExistQuery();

            return await _mediator.Send(new GetUserAsignedClassesQuery(userId));
        }

        public async Task<bool> SoftDeleteAsync(int classId, int userId)
        {
            var courseAssigned = _repository.FilterAsNoTracking<Course>(c => c.ClassId == classId && c.UserId == userId);

            await courseAssigned.CheckIfExistQuery();

            using (var transaction = _repository.GetContext().Database.BeginTransaction())
            {
                try
                {
                    await _mediator.Send(new SoftDeleteCourseCommand(classId, userId));

                    transaction.Commit();

                    return true;
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    throw new Exception(e.Message, e.InnerException);
                }
            }
        }
    }
}
