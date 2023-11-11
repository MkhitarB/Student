using Student.DTO.ViewModels.Bases;
using Student.Infrastructure.Validators;
using System.ComponentModel.DataAnnotations;

namespace Student.DTO.ViewModels.Users.ReqModels
{
    public class RegisterStudentReqModel : IViewModel
    {
        [Required]
        [StringValidator(3)]
        public string FirstName { get; set; }
        [Required]
        [StringValidator(3)]
        public string LastName { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        [EmailValidator]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string ConfirmedPassword { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string Address { get; set; } 
    }
}
