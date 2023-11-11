using MediatR;
using Microsoft.EntityFrameworkCore;
using Student.BLL.Services.UserService.Services.Commands.Create;
using Student.DAL.Repository;
using Student.Entity.Entities;
using Student.Infrastructure.Enums.EntityEnums;
using Student.Infrastructure.Helpers.Utilities;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Student.BLL.Services.UserService.Services.Commands.Update
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, bool>
    {
        private readonly IEntityRepository _repository;
        public UpdateUserCommandHandler(IEntityRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(UpdateUserCommand command, CancellationToken token)
        {
            try
            {
                var user = await _repository.Filter<User>(u => u.Id == command.id).FirstOrDefaultAsync();
                dynamic property = new ExpandoObject();

                if (command.firstName != null && command.lastName != null)
                {
                    property.FirstName = command.firstName;
                    property.LastName = command.lastName;
                    property.FullName = $"{command.firstName} {command.lastName}";
                }
                else
                {
                    if (command.firstName != null)
                    {
                        property.FirstName = command.firstName;
                        property.FullName = $"{command.firstName} {user.LastName}";

                    }

                    if (command.lastName != null)
                    {
                        property.LastName = command.lastName;
                        property.FullName = $"{user.FirstName} {command.lastName}";
                    }
                }

               

                if (command.email != null)
                {
                    property.Email = command.email;
                }

                if (command.address != null)
                {
                    property.Address = command.address;
                }

                if (command.date != null)
                {
                    property.DateOfBirth = command.date?.ToString("MM/dd/yyyy");
                }

                if (command.password != null)
                {
                    property.Password = command.password.HashPassword();
                }

                if (command.phoneNumber != null)
                {
                    property.PhoneNumber = command.phoneNumber;
                }

                _repository.Update<User>(user, property);

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
