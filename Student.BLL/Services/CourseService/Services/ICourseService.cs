using Student.DTO.ViewModels.Courses.ResModels;

namespace Student.BLL.Services.CourseService.Services
{
    public interface ICourseService
    {
        Task<bool> AssignAsync(int classId, int userId);
        Task<List<GetUserWithAssignedClassesResModel>> GetUserAsignedClassesAsync(int userId);
        Task<bool> SoftDeleteAsync(int classId, int userId);
    }
}
