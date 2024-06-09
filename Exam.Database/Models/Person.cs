using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Exam.Database.Enums;

namespace Exam.Database.Models
{
    public class Person

    {
        [Key]
        public int Id { get; set; }

        [Required]
        //  [NameAttribute(ErrorMessage = "Invalid name.")]
        public string Name { get; set; }

        [Required]
        // [LastNameAttribute(ErrorMessage = "Invalid last name.")]
        public string LastName { get; set; }

        [Required]
        // [GenderAttribute(ErrorMessage = "Invalid gender.")]
        public Gender Gender { get; set; }

        [Required]
        // [BirthdayAttribute(ErrorMessage = "Invalid birthday.")]
        public DateTime Birthday { get; set; }

        [Required]
        //  [PersonalIdAttribute(ErrorMessage = "Invalid personal identification code.")]
        public string PersonalId { get; set; }

        [Required]
        // [PhoneNumberAttribute(ErrorMessage = "Invalid telephone number.")]
        public string TelephoneNumber { get; set; }

        [Required]
        [EmailAddress]
        // [EmailDomainAttribute(ErrorMessage = "Invalid email.")]
        public string Email { get; set; }

        [Required]
        //  [ProfilePhotoAttribute(ErrorMessage = "Invalid profile photo.")]
        public byte[] ProfilePhoto { get; set; }

        [Required]
        [ForeignKey("Address")]
        public int AddressId { get; set; }

        public Address Address { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; }

    }
}
