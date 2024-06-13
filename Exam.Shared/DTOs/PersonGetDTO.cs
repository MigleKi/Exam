using Exam.Database.Enums;


namespace Exam.Shared.DTOs
{
    public class PersonGetDTO
    {

        public string Name { get; set; }

        public string LastName { get; set; }

        public Gender Gender { get; set; }

        public DateTime Birthday { get; set; }

        public string PersonalId { get; set; }

        public string TelephoneNumber { get; set; }

        public string Email { get; set; }
        //public IFormFile ProfilePhoto { get; set; }
        public AddressGetDTO Address { get; set; }
    }
}

