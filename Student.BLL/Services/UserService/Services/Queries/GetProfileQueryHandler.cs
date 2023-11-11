using MediatR;
using Microsoft.EntityFrameworkCore;
using Student.DAL.Repository;
using Student.DTO.Authentication;
using Student.DTO.ViewModels.Users.ResModels;
using Student.Entity.Entities;

namespace Student.BLL.Services.UserService.Services.Queries
{
    public class GetProfileQueryHandler : IRequestHandler<GetProfileQuery, GetProfileResModel>
    {
        private readonly IEntityRepository _repository;
        public GetProfileQueryHandler(IEntityRepository repository)
        {
            _repository = repository;
        }

        public async Task<GetProfileResModel> Handle(GetProfileQuery query, CancellationToken token)
        {
            try
            {
                return await _repository.FilterAsNoTracking<User>(u => u.Id == query.id)
                    .Select(u => new GetProfileResModel
                    {
                        Address = u.Address,
                        DateOfBirth = u.DateOfBirth,
                        Email = u.Email,
                        FullName = u.FullName,
                        PhoneNumber = u.PhoneNumber
                    }).FirstOrDefaultAsync();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e.InnerException);
            }
        }
    }
}
