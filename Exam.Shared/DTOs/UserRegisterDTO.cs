﻿using Exam.Shared.Attributes;
using System.ComponentModel.DataAnnotations;


namespace Exam.Shared.DTOs
{
    public class UserRegisterDTO
    {

        [Required]
        [StringLength(20, MinimumLength = 8, ErrorMessage = "Username must be between 8 and 20 characters.")]
        public string Username { get; set; }
        [Required]
        [PasswordCheck]
        public string Password { get; set; }

    }
}
