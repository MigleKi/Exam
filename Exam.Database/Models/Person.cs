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
        public string Name { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public Gender Gender { get; set; }

        [Required]
        public DateTime Birthday { get; set; }

        [Required]
        public string PersonalId { get; set; }

        [Required]
        public string TelephoneNumber { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
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
