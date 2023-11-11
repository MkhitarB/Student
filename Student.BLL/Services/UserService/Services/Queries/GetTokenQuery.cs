using MediatR;
using Student.DTO.ViewModels.Users.ReqModels;
using Student.DTO.ViewModels.Users.ResModels;

namespace Student.BLL.Services.UserService.Services.Queries
{
    public record GetTokenQuery(GetTokenReqModel model) : IRequest<GetTokenResModel>;
}
