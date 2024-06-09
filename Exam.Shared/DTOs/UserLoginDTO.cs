using Exam.Shared.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam.Shared.DTOs
{
    public class UserLoginDTO
    {
        [Required]
        [StringLength(20, MinimumLength = 8, ErrorMessage = "Username must be between 8 and 20 characters.")]
        public string Username { get; set; }
        [Required]
        [PasswordCheck]
        public string Password { get; set; }
    }
}
