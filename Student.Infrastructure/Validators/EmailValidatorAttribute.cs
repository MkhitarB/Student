using System.ComponentModel.DataAnnotations;
using System.Net.Mail;

namespace Student.Infrastructure.Validators
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
    public class EmailValidatorAttribute : ValidationAttribute
    {
        public EmailValidatorAttribute(string errorMessage = "Email is invalid") : base(errorMessage)
        {
        }

        protected override ValidationResult IsValid(object value, ValidationContext context)
        {
            try
            {
                if(value == null)
                {
                   return  ValidationResult.Success;
                }

                new MailAddress(value.ToString());

                return ValidationResult.Success;
            }
            catch (FormatException)
            {
                return new ValidationResult(ErrorMessage);
            }        
        }
    }
}
