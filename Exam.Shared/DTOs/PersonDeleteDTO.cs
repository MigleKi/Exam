using Exam.Database.Enums;
using System.ComponentModel.DataAnnotations;
using Exam.Shared.Attributes;
using Microsoft.AspNetCore.Http;


namespace Exam.Shared.DTOs
{
    public class PersonDeleteDTO
    {

        [Required]
        [NameCheck]
        public string Name { get; set; }

        [Required]
        [LastNameCheck]
        public string LastName { get; set; }

    }
}

