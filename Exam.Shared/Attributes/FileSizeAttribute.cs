using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Exam.Shared.Attributes
{
    public class FileSizeAttribute : ValidationAttribute
    {
        private readonly int _maxFileSize;
        public FileSizeAttribute(int maxFileSize)
        {
            _maxFileSize = maxFileSize;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is IFormFile file && file.Length > _maxFileSize)
            {
                return new ValidationResult($"Maximum allowed file size is {_maxFileSize} bytes.");
            }

            return ValidationResult.Success;
        }
    }
}
