using Student.DTO.ViewModels.Bases;
using Student.Infrastructure.Validators;

namespace Student.DTO.ViewModels.Users.ReqModels
{
    public class UpdateStudentReqModel : IViewModel
    {
        [StringValidator(3)]
        public string FirstName { get; set; } = null;
        [StringValidator(3)]
        public string LastName { get; set; } = null;
        public DateTime? Date { get; set; } = null;
        [EmailValidator]
        public string Email { get; set; } = null;
        public string Password { get; set; } = null;
        public string ConfirmedPassword { get; set; } = null;
        public string PhoneNumber { get; set; } = null;
        public string Address { get; set; } = null;
    }
}
