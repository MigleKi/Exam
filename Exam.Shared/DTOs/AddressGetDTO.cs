using Exam.Database.Models;
using System.ComponentModel.DataAnnotations;
using Exam.Shared.Attributes;

namespace Exam.Shared.DTOs
{
    public class AddressGetDTO
    {

        public string City { get; set; }

        public string Street { get; set; }

        public string HouseNumber { get; set; }

        public string? ApartmentNumber { get; set; }


    }
}
