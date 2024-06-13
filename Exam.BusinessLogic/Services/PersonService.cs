using AutoMapper;
using Exam.BusinessLogic.Services.Interfaces;
using Exam.Database.Enums;
using Exam.Database.Models;
using Exam.Database.Repositories.Interfaces;
using Exam.Shared.DTOs;
using Microsoft.Extensions.Logging;

namespace Exam.BusinessLogic.Services
{
    public class PersonService : IPersonService
    {
        private readonly IPersonRepository _personRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<PersonService> _logger;

        public PersonService(IPersonRepository personRepository, IMapper mapper, ILogger<PersonService> logger)
        {
            _personRepository = personRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<PersonCreateDTO> CreatePersonAsync(int userId, PersonCreateDTO personCreateDTO)
        {
            _logger.LogInformation($"Creating person for user ID: {userId}");

            var existingPersons = await _personRepository.GetAllPersonsByUserIdAsync(userId);

            if (existingPersons.Any(p => p.PersonalId == personCreateDTO.PersonalId))
            {
                throw new ArgumentException($"Person with this identification code is already created for user {userId}");
            }
            int firstDigit = int.Parse(personCreateDTO.PersonalId[0].ToString());
            if (firstDigit > 2 && firstDigit < 7)
            {
                PersonalCodeCrossCheck(personCreateDTO);
            }

            var person = _mapper.Map<Person>(personCreateDTO);
            person.UserId = userId;


            await _personRepository.CreatePersonAsync(person);

            _logger.LogInformation($"Person created for user ID: {userId}");
            return _mapper.Map<PersonCreateDTO>(person);
        }



        public async Task<PersonUpdateDTO> UpdatePersonAsync(int userId, int personId, PersonUpdateDTO personUpdateDTO)
        {
            _logger.LogInformation($"Update for person ID: {personId} started");

            var existingPerson = await _personRepository.GetByIdAsync(personId);
            if (existingPerson == null)
            {
                _logger.LogWarning($"Person ID: {personId} is not found");
                throw new ArgumentException("Person not found.");
            }
            if (existingPerson.UserId != userId)
            {
                _logger.LogWarning($"User ID: {userId} is not authorized to access person ID: {personId}");
                throw new ArgumentException("Access is denied.");
            }

            int firstDigit = int.Parse(personUpdateDTO.PersonalId[0].ToString());
            if (firstDigit > 2 && firstDigit < 7)
            {
                PersonalCodeCrossCheck(personUpdateDTO);
            }
            _mapper.Map(personUpdateDTO, existingPerson);

            await _personRepository.UpdatePersonAsync(existingPerson);

            _logger.LogInformation($"Person details updated for person ID: {personId} by user ID: {userId}");
            return _mapper.Map<PersonUpdateDTO>(existingPerson);
        }

        public async Task<PersonDeleteDTO> DeletePersonAsync(int personId, int userId)
        {
            _logger.LogInformation($"Deletion for person ID: {personId} started");

            var existingPerson = await _personRepository.GetByIdAsync(personId);
            if (existingPerson == null)
            {
                _logger.LogWarning($"Person ID: {personId} is not found");
                throw new KeyNotFoundException("Person not found");
            }
            if (existingPerson.UserId != userId)
            {
                _logger.LogWarning($"User ID: {userId} is not authorized to remove person ID: {personId}");
                throw new ArgumentException("Access is denied.");
            }

            var deletedPerson = await _personRepository.DeletePersonAsync(personId);
            return _mapper.Map<PersonDeleteDTO>(deletedPerson);
        }


        public async Task<PersonGetDTO> GetByIdAsync(int userId, int personId)
        {
            _logger.LogInformation($"Retrieving person ID: {personId} for user ID: {userId}");
            var person = await _personRepository.GetByIdAsync(personId);
            if (person == null)
            {
                _logger.LogWarning($"Person with ID: {personId} not found.");
                throw new ArgumentException("Person not found.");
            }
            if (person.UserId != userId)
            {
                _logger.LogWarning($"User ID: {userId} is not authorized to access person ID: {personId}");
                throw new ArgumentException("Access is denied.");
            }
            return _mapper.Map<PersonGetDTO>(person);
        }
        private void PersonalCodeCrossCheck(PersonCreateDTO personCreateDTO)
        {
            int genderNumber = int.Parse(personCreateDTO.PersonalId.Substring(0, 1));
            int birthYear = personCreateDTO.Birthday.Year;
            int birthMonth = personCreateDTO.Birthday.Month;
            int birthDay = personCreateDTO.Birthday.Day;
            int lastTwoDigitsOfYear = birthYear % 100;
            int personalCodeYear = int.Parse(personCreateDTO.PersonalId.Substring(1, 2));
            int personalCodemonth = int.Parse(personCreateDTO.PersonalId.Substring(3, 2));
            int personalCodeday = int.Parse(personCreateDTO.PersonalId.Substring(5, 2));

            if (personCreateDTO.Gender == Gender.Male && (genderNumber == 4 || genderNumber == 6))
            {
                throw new ArgumentException("Personal code does not match gender value");
            }
            if (personCreateDTO.Gender == Gender.Female && (genderNumber == 3 || genderNumber == 5))
            {
                throw new ArgumentException("Personal code does not match gender value");
            }
            if (!(lastTwoDigitsOfYear == personalCodeYear && birthMonth == personalCodemonth && birthDay == personalCodeday))
            {
                throw new ArgumentException("Personal code does not match birth date value");
            }


        }
        private void PersonalCodeCrossCheck(PersonUpdateDTO personUpdateDTO)
        {
            int genderNumber = int.Parse(personUpdateDTO.PersonalId.Substring(0, 1));
            int birthYear = personUpdateDTO.Birthday.Year;
            int birthMonth = personUpdateDTO.Birthday.Month;
            int birthDay = personUpdateDTO.Birthday.Day;
            int lastTwoDigitsOfYear = birthYear % 100;
            int personalCodeYear = int.Parse(personUpdateDTO.PersonalId.Substring(1, 2));
            int personalCodemonth = int.Parse(personUpdateDTO.PersonalId.Substring(3, 2));
            int personalCodeday = int.Parse(personUpdateDTO.PersonalId.Substring(5, 2));

            if (!(personUpdateDTO.Gender == Gender.Male && (genderNumber == 4 || genderNumber == 6)))
            {
                throw new ArgumentException("Personal code does not match gender value");
            }
            if (!(personUpdateDTO.Gender == Gender.Female && (genderNumber == 3 || genderNumber == 5)))
            {
                throw new ArgumentException("Personal code does not match gender value");
            }
            if (!(lastTwoDigitsOfYear == personalCodeYear && birthMonth == personalCodemonth && birthDay == personalCodeday))
            {
                throw new ArgumentException("Personal code does not match birth date value");
            }


        }

    }
}
