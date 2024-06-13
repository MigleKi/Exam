using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;


namespace Exam.Shared.Attributes
{
    public class LastNameCheckAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            string lastName = value as string;
            if (lastName != null && lastName.Length >= 2 && lastName.Length <= 50 && Regex.IsMatch(lastName, @"^[a-zA-ZąčęėįšųūžĄČĘĖĮŠŲŪŽ]+$"))
            {
                return ValidationResult.Success;
            }
            return new ValidationResult("Invalid last name");
        }
    }
}
