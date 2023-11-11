using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Student.BLL.Services.UserService.Services;
using Student.DTO.ViewModels.Paginations;
using Student.DTO.ViewModels.Users.ReqModels;
using System.ComponentModel.DataAnnotations;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Student.Controllers
{
    public class UserController : BaseController
    {
        private readonly IUserService _service;
        public UserController(IUserService service)
        {
            _service = service;
        }
        //1- First part [User]:
        //-	Registration Form.
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> RegisterStudentAsync([FromBody] RegisterStudentReqModel model)
        {
            return await MakeActionCallWithModelAsync(async () => await _service.RegisterStudentAsync(model), model);
        }

        //1- First part [User]:
        //-	Login Form.
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetTokenAsync([FromQuery] GetTokenReqModel model)
        {
            return await MakeActionCallWithModelAsync(async () => await _service.GetTokenAsync(model), model);
        }

        //1- First part [User]:
        //-	Profile Form.
        [HttpGet]
        public async Task<IActionResult> GetProfileAsync()
        {
            return await MakeActionCallAsync(async () => await _service.GetProfileAsync(GetPersonId()));
        }
        //1- First part [User]:
        //-	One dashboard to show all users information.
        [HttpGet]
        public async Task<IActionResult> GetListAsync([FromQuery] PagingRequest model)
        {
            return await MakeActionCallWithModelAsync(async () => await _service.GetListAsync(model, GetPersonId()), model);
        }

        //2-Second Part [Classes]:
        //-	Show all users and the assigned class for each one.
        [HttpGet]
        public async Task<IActionResult> GetListWithAssignedCourseAsync([FromQuery] PagingRequest model)
        {
            return await MakeActionCallWithModelAsync(async () => await _service.GetListWithAssignedCourseAsync(model, GetPersonId()), model);
        }

        //1- First part [User]:
        //-	Edit option for each added user.S
        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] UpdateStudentReqModel model)
        {
            return await MakeActionCallWithModelAsync(async () => await _service.UpdateAsync(model, GetPersonId()), model);
        }

        //1- First part [User]:
        //delete option for each added user.
        [HttpDelete]
        public async Task<IActionResult> DeleteAsync()
        {
            return await MakeActionCallAsync(async () => await _service.SoftDeleteAsync(GetPersonId()));
        }
    }
}
