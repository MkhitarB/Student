using MediatR;
using Student.DTO.ViewModels.Courses.ResModels;

namespace Student.BLL.Services.CourseService.Services.Queries
{
    public record GetUserAsignedClassesQuery(int userId) : IRequest<List<GetUserWithAssignedClassesResModel>>;
}
