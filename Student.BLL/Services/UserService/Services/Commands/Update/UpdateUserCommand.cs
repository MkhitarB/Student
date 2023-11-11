using MediatR;

namespace Student.BLL.Services.UserService.Services.Commands.Update
{
    public record UpdateUserCommand(int id, string firstName, string lastName, DateTime? date, string email, string password, string phoneNumber, string address) : IRequest<bool>;
}
