using System.ComponentModel.DataAnnotations;

namespace Exam.Shared.Attributes
{
    public class BirthdayAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is DateTime birthday && birthday < DateTime.Now)
            {
                return ValidationResult.Success;
            }
            return new ValidationResult("Invalid date format");
        }
    }
}
