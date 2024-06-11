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


            //if (personCreateDTO.ProfilePhoto != null)
            //{
            //    var filename = ImageHelper.GenerateImageFileName(person.Name, person.LastName);
            //    var filePath = ImageHelper.GetImageFilePath(filename);
            //    ImageHelper.SaveResizedImage(filePath, personCreateDTO.ProfilePhoto, 200, 200);
            //    person.ProfilePhotoPath = filePath;
            //}

            await _personRepository.CreatePersonAsync(person);

            //var address = _mapper.Map<Person>(personCreateDTO.Address);
            //address.PersonId = person.Id;

            //await _personRepository.CreatePlaceOfResidenceAsync(placeOfResidence);

            _logger.LogInformation($"Person created for user ID: {userId}");
            return _mapper.Map<PersonCreateDTO>(person);
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

        public async Task<PersonUpdateDTO> UpdatePersonDetailsAsync(int userId, int personId, PersonUpdateDTO personUpdateDetailsDTO)
        {
            throw new NotImplementedException();
        }
    }
}
