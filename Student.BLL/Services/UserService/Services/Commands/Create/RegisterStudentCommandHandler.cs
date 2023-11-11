using MediatR;
using Student.DAL.Repository;
using Student.Entity.Entities;
using Student.Infrastructure.Enums.EntityEnums;
using Student.Infrastructure.Helpers.Utilities;

namespace Student.BLL.Services.UserService.Services.Commands.Create
{
    public class RegisterStudentCommandHandler : IRequestHandler<RegisterStudentCommand, bool>
    {
        private readonly IEntityRepository _repository;
        public RegisterStudentCommandHandler(IEntityRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(RegisterStudentCommand command, CancellationToken token)
        {
            try
            {
                await _repository.CreateAsync(new User
                {
                    FirstName = command.firstName,
                    LastName = command.lastName,
                    FullName = $"{command.firstName} {command.lastName}",
                    Email = command.email,
                    Address = command.adddress,
                    DateOfBirth = command.date.ToString("MM/dd/yyyy"),
                    Password = command.password.HashPassword(),
                    PhoneNumber = command.phoneNumber,
                    Type = UserType.Student,
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
