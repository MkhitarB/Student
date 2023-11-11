using Student.DTO.ViewModels.Bases;
using Student.Infrastructure.Validators;
using System.ComponentModel.DataAnnotations;

namespace Student.DTO.ViewModels.Users.ReqModels
{
    public class GetTokenReqModel : IViewModel
    {
        [Required]
        [EmailValidator]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
