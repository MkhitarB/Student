using Microsoft.AspNetCore.Mvc;
using Student.BLL.Services.CourseService.Services;
using Student.BLL.Services.UserService.Services;
using Student.Controllers;

namespace Student.API.Controllers
{
    public class CourseController : BaseController
    {
        private readonly ICourseService _service;
        public CourseController(ICourseService service)
        {
            _service = service;
        }

        [HttpPost]
        [Route("{classId}")]
        public async Task<IActionResult> AssignAsync(int classId)
        {
            return await MakeActionCallAsync(async () => await _service.AssignAsync(classId, GetPersonId()));
        }

        [HttpGet]
        public async Task<IActionResult> GetUserAsignedClassesAsync()
        {
            return await MakeActionCallAsync(async () => await _service.GetUserAsignedClassesAsync(GetPersonId()));
        }

        [HttpDelete]
        [Route("{classId}")]
        public async Task<IActionResult> DeleteAsync(int classId)
        {
            return await MakeActionCallAsync(async () => await _service.SoftDeleteAsync(classId, GetPersonId()));
        }
    }
}
