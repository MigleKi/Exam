using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace Exam.Database.Models
{
    [Table("Users")]
    [Index(nameof(Username), IsUnique = true)]
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Username { get; set; }


        [Required]
        public byte[]? PasswordHash { get; set; }

        [Required]
        public byte[]? PasswordSalt { get; set; }

        [Required]
        public string Role { get; set; }

        public ICollection<Person>? Persons { get; set; }

    }

}
