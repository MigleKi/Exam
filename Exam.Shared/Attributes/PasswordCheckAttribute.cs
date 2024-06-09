using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Exam.Shared.Attributes
{
    public class PasswordCheckAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            string password = value as string;
            if (password != null && password.Length >= 12 &&
                Regex.IsMatch(password, @"[A-Z].*[A-Z]") && // At least two uppercase letters
                Regex.IsMatch(password, @"[a-z].*[a-z]") && // At least two lowercase letters
                Regex.IsMatch(password, @"[0-9].*[0-9]") && // At least two digits
                Regex.IsMatch(password, @"[\W_].*[\W_]"))   // At least two special characters
            {
                return ValidationResult.Success;
            }
            return new ValidationResult("Password must be at least 12 characters long and contain at least two uppercase letters, two lowercase letters, two digits, and two special characters.");
        }
    }
}
