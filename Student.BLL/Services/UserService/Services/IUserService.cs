using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Student.BLL.Services.UserService.Services
{
    public interface IUserService
    {
        Task<bool> RegisterAsync();
    }
}
