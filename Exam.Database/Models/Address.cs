using System.ComponentModel.DataAnnotations;


namespace Exam.Database.Models
{
    public class Address
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string City { get; set; }

        [Required]
        public string Street { get; set; }

        [Required]
        public string HouseNumber { get; set; }

        public string? ApartmentNumber { get; set; }

        [Required]
        public int PersonId { get; set; }

        public Person Person { get; set; }

    }
}
