using MediatR;

namespace Student.BLL.Services.CourseService.Services.Commands.Delete
{
    public record SoftDeleteCourseCommand(int classId, int userId) : IRequest<bool>;
}
