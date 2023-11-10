using Student.BLL.Mediator;
using Student.DAL.Repository;
using Student.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Student.BLL.Services.UserService.Services
{
    public class UserService : IUserService
    {
        private readonly IEntityRepository _repostiory;
        public UserService(IEntityRepository repostiory)
        {
            _repostiory = repostiory;
        }

        public async Task<bool> RegisterAsync()
        {
            await _repostiory.CreateAsync(new User
            {
                 
            });
            await _repostiory.SaveChangesAsync();
            return true;
        }
    }
}
