using MediatR;

namespace Student.BLL.Services.UserService.Services.Commands.Create
{
    public record RegisterStudentCommand(string firstName, string lastName, DateTime date, string email, string password, string phoneNumber, string adddress) : IRequest<bool>;
}