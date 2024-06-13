using System.ComponentModel.DataAnnotations;

namespace Exam.Shared.Attributes
{
    public class BirthdayAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {

            if (value is DateTime birthday)
            {
                DateTime currentDate = DateTime.Now;
                DateTime hundredYearsAgo = currentDate.AddYears(-100);

                if (birthday <= currentDate && birthday >= hundredYearsAgo)
                {
                    return ValidationResult.Success;
                }
                else if (birthday > currentDate)
                {
                    return new ValidationResult("The entered date cannot be in the future.");
                }
                else
                {
                    return new ValidationResult("The entered date cannot be older than 100 years.");
                }
            }
            return new ValidationResult("Invalid date format");
        }
    }
}
