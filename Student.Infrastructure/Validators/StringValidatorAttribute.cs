using System.ComponentModel.DataAnnotations;

namespace Student.Infrastructure.Validators
{
    public class StringValidatorAttribute : ValidationAttribute
    {
        private readonly int _minLength;
        private readonly int _maxLength;
        public StringValidatorAttribute(int minLength, int maxLength = 0, string errorMessage = "String value is invalid") : base(errorMessage)
        {
            _minLength = minLength;
            _maxLength = maxLength;
        }

        protected override ValidationResult IsValid(object value, ValidationContext context)
        {
            if (value == null)
                return ValidationResult.Success;
            var stringValue = value.ToString();
            if (string.IsNullOrEmpty(stringValue) || stringValue.Length < _minLength)
                return new ValidationResult(ErrorMessage);
            if (_maxLength > 0 ? stringValue.Length > _maxLength : false)
                return new ValidationResult(ErrorMessage);
            return ValidationResult.Success;
        }
    }
}
