using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Exam.Shared.Attributes
{
    public class StreetAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            string street = value as string;
            if (street != null && street.Contains(" ") && Regex.IsMatch(street, @"^[a-zA-Z0-9ąčęėįšųūžĄČĘĖĮŠŲŪŽ ]+$"))
            {
                return ValidationResult.Success;
            }
            return new ValidationResult("Invalid street name");
        }
    }
}
