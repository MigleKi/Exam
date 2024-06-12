using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Exam.Shared.Attributes
{
    public class FileAllowedExtensionsAttribute : ValidationAttribute
    {
        private readonly string[] _extensions;
        public FileAllowedExtensionsAttribute(string[] extensions)
        {
            _extensions = extensions;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {

            if (value is IFormFile file)
            {
                var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
                if (!_extensions.Contains(extension.ToLower()))
                {
                    return new ValidationResult(GetErrorMessage());
                }
            }
            return ValidationResult.Success;
        }

        public string GetErrorMessage()
        {
            return $"This file extension is not allowed! Allowed file types are: {string.Join(", ", _extensions)}";
        }
    }
}
