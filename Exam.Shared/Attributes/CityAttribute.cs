using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Exam.Shared.Attributes
{
    public class CityAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            string city = value as string;
            if (city != null && city.Length <= 20 && Regex.IsMatch(city, @"^[a-zA-ZąčęėįšųūžĄČĘĖĮŠŲŪŽ ]+$"))
            {
                return ValidationResult.Success;
            }
            return new ValidationResult("Invalid city name");
            //Add list of Lithuanian cities ? 
        }
    }
}
