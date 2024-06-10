using Exam.Database.Models;
using System.ComponentModel.DataAnnotations;
using Exam.Shared.Attributes;

namespace Exam.Shared.DTOs
{
    public class AddressCreateDTO
    {
        [Required]
        [City]
        public string City { get; set; }

        [Required]
        [Street]
        public string Street { get; set; }

        [Required]
        [StringLength(6)]
        [RegularExpression(@"^[\d\w\-]+$", ErrorMessage = "House number is not valid")]
        public string HouseNumber { get; set; }

        [StringLength(6)]
        [RegularExpression(@"^[\d\w\-]+$", ErrorMessage = "Apartment number is not valid")]
        public string? ApartmentNumber { get; set; }

        [Required]
        public int PersonId { get; set; }

        public Person Person { get; set; }

    }
}
