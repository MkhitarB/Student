using Student.DTO.ViewModels.Paginations;
using Student.DTO.ViewModels.Users.ReqModels;
using Student.DTO.ViewModels.Users.ResModels;

namespace Student.BLL.Services.UserService.Services
{
    public interface IUserService
    {
        Task<bool> RegisterStudentAsync(RegisterStudentReqModel model);
        Task<GetTokenResModel> GetTokenAsync(GetTokenReqModel model);
        Task<GetProfileResModel> GetProfileAsync(int id);
        Task<PagingResponse<GetStudentListResModel>> GetListAsync(PagingRequest model, int id);
        Task<PagingResponse<GetStudentListWithAssignedClassesResModel>> GetListWithAssignedCourseAsync(PagingRequest model, int id);
        Task<bool> UpdateAsync(UpdateStudentReqModel model, int id);
        Task<bool> SoftDeleteAsync(int id);

    }
}
