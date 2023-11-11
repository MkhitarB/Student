using MediatR;
using Student.BLL.Services.CourseService.Services.Queries;
using Student.DAL.Repository;
using Student.DTO.ViewModels.Courses.ResModels;
using Student.Entity.Entities;

namespace Student.BLL.Services.CourseService.Services.Commands.Create
{
    public class AssignACourseCommandHandler : IRequestHandler<AssignACourseCommand, bool>
    {
        private readonly IEntityRepository _repository;
        public AssignACourseCommandHandler(IEntityRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(AssignACourseCommand query, CancellationToken token)
        {
            try
            {
                await _repository.CreateAsync(new Course
                {
                    ClassId = query.classId,
                    UserId = query.userId,
                });

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
