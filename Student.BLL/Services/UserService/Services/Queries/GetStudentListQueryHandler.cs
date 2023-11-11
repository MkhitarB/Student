using MediatR;
using Student.BLL.Extensions;
using Student.DAL.Repository;
using Student.DTO.ViewModels.Paginations;
using Student.DTO.ViewModels.Users.ResModels;
using Student.Entity.Entities;
using Student.Infrastructure.Enums.EntityEnums;

namespace Student.BLL.Services.UserService.Services.Queries
{
    public class GetStudentListQueryHandler : IRequestHandler<GetStudentListQuery, PagingResponse<GetStudentListResModel>>
    {
        private readonly IEntityRepository _repository;
        public GetStudentListQueryHandler(IEntityRepository repository)
        {
            _repository = repository;
        }

        public async Task<PagingResponse<GetStudentListResModel>> Handle(GetStudentListQuery query, CancellationToken token)
        {
            try
            {
                return await _repository.FilterAsNoTracking<User>(u => u.Type == UserType.Student)
                    .Select(u => new GetStudentListResModel
                    {
                        Address = u.Address,
                        DateOfBirth = u.DateOfBirth,
                        Email = u.Email,
                        FullName = u.FullName,
                        PhoneNumber = u.PhoneNumber
                    }).ToPagingAsync(query.model);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e.InnerException);
            }
        }
    }
}
