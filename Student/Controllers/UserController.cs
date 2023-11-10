using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Student.BLL.Services.UserService.Services;

namespace Student.Controllers
{
    public class UserController : BaseController
    {
        private readonly IUserService _service;
        public UserController(IUserService service)
        {
            _service = service;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<bool> RegisterAsync()
        {
            return await _service.RegisterAsync();
        }
    }
}
