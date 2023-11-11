using MediatR;

namespace Student.BLL.Services.CourseService.Services.Commands.Create
{
    public record AssignACourseCommand(int classId, int userId) : IRequest<bool>;
}
