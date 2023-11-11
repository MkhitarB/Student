using MediatR;
using Microsoft.EntityFrameworkCore;
using Student.DAL.Repository;
using Student.DTO.ViewModels.Courses.ResModels;
using Student.Entity.Entities;

namespace Student.BLL.Services.CourseService.Services.Queries
{
    public class GetUserAsignedClassesQueryHandler : IRequestHandler<GetUserAsignedClassesQuery, List<GetUserWithAssignedClassesResModel>>
    {
        private readonly IEntityRepository _repository;
        public GetUserAsignedClassesQueryHandler(IEntityRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<GetUserWithAssignedClassesResModel>> Handle(GetUserAsignedClassesQuery query, CancellationToken token)
        {
            try
            {
                return await _repository.FilterAsNoTracking<Class>(c => c.Id != default)
                    .Select(c => new GetUserWithAssignedClassesResModel
                    {
                        ClassId = c.Id,
                        CourseName = c.Name,
                        IsAssigned = c.Courses.Any(c => c.UserId == query.userId)
                    }).ToListAsync();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e.InnerException);
            }
        }
    }
}
