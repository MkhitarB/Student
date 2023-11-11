using MediatR;

namespace Student.BLL.Services.UserService.Services.Commands.Delete
{
    public record SoftDeleteStudentCommand(int id) : IRequest<bool>;
}
