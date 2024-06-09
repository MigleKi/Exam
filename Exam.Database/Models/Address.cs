using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Exam.Database.Models
{
    public class Address
    {
        [Key]
        public int Id { get; set; }
        [Required]
        // [CityAttribute(ErrorMessage = "Invalid city name.")]
        public string City { get; set; }

        [Required]
        //[StreetAttribute(ErrorMessage = "Invalid street name.")]
        public string Street { get; set; }

        [Required]
        //[HouseNumberAttribute(ErrorMessage = "Invalid house number.")]
        public string HouseNumber { get; set; }

        // [ApartmentNumberAttribute(ErrorMessage = "Invalid apartment number.")]
        public string? ApartmentNumber { get; set; }

        [Required]
        public int PersonId { get; set; }

        public Person Person { get; set; }

    }
}
