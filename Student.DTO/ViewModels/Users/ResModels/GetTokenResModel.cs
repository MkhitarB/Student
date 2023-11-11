using Student.Infrastructure.Enums.EntityEnums;

namespace Student.DTO.ViewModels.Users.ResModels
{
    public class GetTokenResModel
    {
        public UserType UserType { get; set; }
        public string Token { get; set; }
        public DateTime ExpireDate { get; set; }
    }
}
