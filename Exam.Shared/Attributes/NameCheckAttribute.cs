using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;


namespace Exam.Shared.Attributes
{
    public class NameCheckAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            string name = value as string;
            if (name != null && name.Length >= 2 && name.Length <= 50 && Regex.IsMatch(name, @"^[a-zA-ZąčęėįšųūžĄČĘĖĮŠŲŪŽ]+$"))
            {
                return ValidationResult.Success;
            }
            return new ValidationResult("Invalid name");
        }
    }
}
