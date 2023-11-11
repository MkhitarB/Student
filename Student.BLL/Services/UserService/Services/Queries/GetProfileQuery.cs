using MediatR;
using Student.DTO.ViewModels.Users.ResModels;

namespace Student.BLL.Services.UserService.Services.Queries
{
    public record GetProfileQuery(int id) : IRequest<GetProfileResModel>;
}
