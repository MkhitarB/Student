using MediatR;
using Student.DTO.ViewModels.Paginations;
using Student.DTO.ViewModels.Users.ResModels;

namespace Student.BLL.Services.UserService.Services.Queries
{
    public record GetStudentListWithAssignedClassesQuery(PagingRequest paging) : IRequest<PagingResponse<GetStudentListWithAssignedClassesResModel>>;
}
