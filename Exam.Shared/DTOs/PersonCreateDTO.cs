using Exam.Database.Enums;
using System.ComponentModel.DataAnnotations;
using Exam.Shared.Attributes;
using Microsoft.AspNetCore.Http;


namespace Exam.Shared.DTOs
{
    public class PersonCreateDTO
    {

        [Required]
        [NameCheck]
        public string Name { get; set; }

        [Required]
        [LastNameCheck]
        public string LastName { get; set; }

        [Required]
        [EnumDataType(typeof(Gender), ErrorMessage = "Gender must be either 'Male' or 'Female'")]
        public Gender Gender { get; set; }

        [Required]
        [Birthday]
        public DateTime Birthday { get; set; }

        [Required]
        [PersonalId]
        public string PersonalId { get; set; }

        [Required]
        [PhoneNumber]
        public string TelephoneNumber { get; set; }

        [Required]
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.(com|net|org|gov|lt)$", ErrorMessage = "Invalid email address")]
        public string Email { get; set; }

        [Required]
        [FileSize(4 * 1024 * 1024)]
        [AllowedExtensions(new string[] { ".jpg", ".jpeg", ".png" })]
        public IFormFile ProfilePhoto { get; set; }
    }
}

