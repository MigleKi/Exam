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
                throw new ArgumentException($"Person is already created for user {userId}");
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

            _mapper.Map(personUpdateDTO, existingPerson);

            //if (Enum.TryParse(personUpdateDTO.Gender, out Gender gender))
            //{
            //    existingPerson.Gender = gender;
            //}
            //else
            //{
            //    _logger.LogWarning($"Invalid gender value: {personUpdateDetailsDTO.Gender} for person ID: {personId}");
            //    throw new ArgumentException("Invalid gender value.");
            //}

            //if (person.PlaceOfResidence != null)
            //{
            //    _mapper.Map(personUpdateDetailsDTO.PlaceOfResidence, person.PlaceOfResidence);
            //}

            await _personRepository.UpdatePersonAsync(existingPerson);
            //await _personRepository.UpdatePlaceOfResidenceAsync(person.PlaceOfResidence);

            _logger.LogInformation($"Person details updated for person ID: {personId} by user ID: {userId}");
            return _mapper.Map<PersonUpdateDTO>(existingPerson);
        }

        public async Task<PersonDeleteDTO> DeletePersonAsync(int userId, int personId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<PersonGetDTO>> GetAllPersonsByUserIdAsync(int userId)
        {
            throw new NotImplementedException();
        }

        public async Task<PersonGetDTO> GetByIdAsync(int userId, int personId)
        {
            throw new NotImplementedException();
        }


    }
}
