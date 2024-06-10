using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;


namespace Exam.Shared.Attributes
{
    public class PhoneNumberAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            string phoneNumber = value as string;
            if (phoneNumber != null && Regex.IsMatch(phoneNumber, @"^\+370[0-9]{8}$"))
            {
                return ValidationResult.Success;
            }
            return new ValidationResult("Invalid phone number");
        }
    }
}
