using Student.BLL.Extensions;
using Student.BLL.Mediator;
using Student.BLL.Services.UserService.Services.Commands.Create;
using Student.BLL.Services.UserService.Services.Commands.Delete;
using Student.BLL.Services.UserService.Services.Commands.Update;
using Student.BLL.Services.UserService.Services.Queries;
using Student.DAL.Repository;
using Student.DTO.ViewModels.Paginations;
using Student.DTO.ViewModels.Users.ReqModels;
using Student.DTO.ViewModels.Users.ResModels;
using Student.Entity.Entities;
using Student.Infrastructure.Enums.EntityEnums;
using Student.Infrastructure.Exceptions;
using Student.Infrastructure.Helpers.Utilities;
using System.Reflection.Metadata.Ecma335;

namespace Student.BLL.Services.UserService.Services
{
    public class UserService : IUserService
    {
        private readonly IEntityRepository _repository;
        private readonly IMediatorHandler _mediator;
        public UserService(IEntityRepository repository, IMediatorHandler mediator)
        {
            _repository = repository;
            _mediator = mediator;
        }

        public async Task<bool> RegisterStudentAsync(RegisterStudentReqModel model)
        {
            var user = _repository.FilterAsNoTracking<User>(f => f.Email == model.Email);
            await user.CheckIfNotExistQuery();

            if (!model.Password.Equals(model.ConfirmedPassword))
                throw new SmartException("Passwords do not match!");

            var phone = Util.ValidatePhoneNumberAndFormat(model.PhoneNumber);
            if (phone == null)
            {
                throw new SmartException("Phone Number is Incorrect!");
            }

            var formatedAddress = Util.FormatAddress(model.Address);

            using (var transaction = _repository.GetContext().Database.BeginTransaction())
            {
                try
                {
                    await _mediator.Send(new RegisterStudentCommand(model.FirstName, model.LastName, model.Date, model.Email, model.Password, phone, formatedAddress));


                    transaction.Commit();

                    return true;
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    throw new Exception(e.Message, e.InnerException);
                }
            }

        }

        public async Task<GetTokenResModel> GetTokenAsync(GetTokenReqModel model)
        {
            return await _mediator.Send(new GetTokenQuery(model));
        }

        public async Task<GetProfileResModel> GetProfileAsync(int id)
        {
            var user = _repository.FilterAsNoTracking<User>(u => u.Id == id);
            await user.CheckIfExistQuery();

            return await _mediator.Send(new GetProfileQuery(id));
        }

        public async Task<PagingResponse<GetStudentListResModel>> GetListAsync(PagingRequest model, int id)
        {
            var user = _repository.FilterAsNoTracking<User>(u => u.Id == id && u.Type == UserType.Admin);
            await user.CheckIfExistQuery();

            return await _mediator.Send(new GetStudentListQuery(model));
        }

        public async Task<PagingResponse<GetStudentListWithAssignedClassesResModel>> GetListWithAssignedCourseAsync(PagingRequest model, int id)
        {
            var user = _repository.FilterAsNoTracking<User>(u => u.Id == id && u.Type == UserType.Admin);
            await user.CheckIfExistQuery();

            return await _mediator.Send(new GetStudentListWithAssignedClassesQuery(model));
        }
        public async Task<bool> UpdateAsync(UpdateStudentReqModel model, int id)
        {
            if (model.FirstName == null && model.LastName == null 
                && model.Email == null && model.Date == null 
                && model.Password == null && model.ConfirmedPassword == null 
                && model.Address == null && model.PhoneNumber == null)
            {
                throw new SmartException("No Data Has Been Updated!");
            }

            var user = _repository.FilterAsNoTracking<User>(u => u.Id == id);
            await user.CheckIfExistQuery();

            if (model.Email != null)
            {
                var userWithNewEmail = _repository.FilterAsNoTracking<User>(f => f.Email == model.Email);
                await user.CheckIfNotExistQuery();
            }
           

            if (model.Password != null || model.ConfirmedPassword != null)
            {
                if (!string.Equals(model.Password, model.ConfirmedPassword))
                {
                    throw new SmartException("Passwords do not match!");
                }
            }



            string phone = null;
            bool isPhoneChecked = false;
            if (model.PhoneNumber != null)
            {
                phone = Util.ValidatePhoneNumberAndFormat(model.PhoneNumber);
                isPhoneChecked = true;
            }

            if (phone == null && isPhoneChecked)
            {
                throw new SmartException("Phone Number is Incorrect!");
            }

            string formatedAddress = null;

            if (model.Address != null)
            {
                formatedAddress = Util.FormatAddress(model.Address);
            }


            using (var transaction = _repository.GetContext().Database.BeginTransaction())
            {
                try
                {
                    await _mediator.Send(new UpdateUserCommand(id, model.FirstName, model.LastName, model.Date, model.Email, model.Password, phone, formatedAddress));

                    transaction.Commit();

                    return true;
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    throw new Exception(e.Message, e.InnerException);
                }
            }
        }
        public async Task<bool> SoftDeleteAsync(int id)
        {
            var user = _repository.FilterAsNoTracking<User>(u => u.Id == id && u.Type == UserType.Student);
            await user.CheckIfExistQuery();
            using (var transaction = _repository.GetContext().Database.BeginTransaction())
            {
                try
                {
                    var res = await _mediator.Send(new SoftDeleteStudentCommand(id));

                    transaction.Commit();

                    return res;
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    throw new Exception(e.Message, e.InnerException);
                }
            }
        }
    }
}
