using MediatR;
using Microsoft.EntityFrameworkCore;
using Student.BLL.Services.UserService.Services.Queries;
using Student.DAL.Repository;
using Student.DTO.ViewModels.Users.ResModels;
using Student.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Student.BLL.Services.UserService.Services.Commands.Delete
{
    public class SoftDeleteStudentCommandHandler : IRequestHandler<SoftDeleteStudentCommand, bool>
    {
        private readonly IEntityRepository _repository;
        public SoftDeleteStudentCommandHandler(IEntityRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(SoftDeleteStudentCommand query, CancellationToken token)
        {
            try
            {
                var userAndCourseIds = await _repository.FilterAsNoTracking<User>(u => u.Id == query.id)
                    .Select(u => new
                    {
                        u.Id,
                        CourseIds = u.Courses.Select(c => c.Id).ToList(),
                    }).FirstOrDefaultAsync();

                if(userAndCourseIds != null)
                {
                    await _repository.RemoveAsync<User>(userAndCourseIds.Id);
                    await _repository.RemoveRangeAsync<Course>(userAndCourseIds.CourseIds);
                    await _repository.SaveChangesAsync();

                    return true;
                }
                else
                {
                    return false;
                }               
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e.InnerException);
            }
        }
    }
}
