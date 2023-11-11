using MediatR;
using Student.DTO.ViewModels.Paginations;
using Student.DTO.ViewModels.Users.ResModels;

namespace Student.BLL.Services.UserService.Services.Queries
{
    public record GetStudentListQuery(PagingRequest model) : IRequest<PagingResponse<GetStudentListResModel>>;
}
