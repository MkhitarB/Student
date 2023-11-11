using MediatR;
using Microsoft.EntityFrameworkCore;
using Student.BLL.Services.CourseService.Services.Commands.Create;
using Student.DAL.Repository;
using Student.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Student.BLL.Services.CourseService.Services.Commands.Delete
{
    public class SoftDeleteCourseCommandHandler : IRequestHandler<SoftDeleteCourseCommand, bool>
    {
        private readonly IEntityRepository _repository;
        public SoftDeleteCourseCommandHandler(IEntityRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(SoftDeleteCourseCommand query, CancellationToken token)
        {
            try
            {
                var courseId = await _repository.FilterAsNoTracking<Course>(c => c.UserId == query.userId && c.ClassId == query.classId)
                    .Select(c => c.Id).FirstOrDefaultAsync();

                await _repository.RemoveAsync<Course>(courseId);    

                await _repository.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e.InnerException);
            }
        }
    }
}
