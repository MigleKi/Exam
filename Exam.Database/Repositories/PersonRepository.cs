using Exam.Database.Models;
using Exam.Database.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Exam.Database.Repositories
{
    public class PersonRepository : IPersonRepository
    {
        private readonly MainDbContext _dbContext;
        private readonly ILogger<PersonRepository> _logger;

        public PersonRepository(MainDbContext dbContext, ILogger<PersonRepository> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }
        public async Task CreatePersonAsync(Person person)
        {
            _logger.LogInformation($"Adding new person: {person.Id} to the database");
            await _dbContext.Persons.AddAsync(person);
            await _dbContext.SaveChangesAsync();
        }
        //public async Task CreateAddressAsync(Address address)
        //{
        //    _logger.LogInformation($"Adding address: {address.Id} for person {address.PersonId} to the database");
        //    await _dbContext.Addresses.AddAsync(address);
        //    await _dbContext.SaveChangesAsync();
        //}
        public async Task DeletePersonAsync(int personId)
        {
            var person = await _dbContext.Persons.FindAsync(personId);

            if (person == null)
            {
                _logger.LogWarning($"Person with {personId} not found in database");
                throw new ArgumentException($"Person with {personId} not found");
            }

            //await DeleteAddressAsync(person.AddressId);

            _dbContext.Persons.Remove(person);
            _logger.LogInformation($"Person {person.Id} has been deleted from database");
            await _dbContext.SaveChangesAsync();


        }
        //public async Task DeleteAddressAsync(int addressId)
        //{
        //    var address = await _dbContext.Addresses
        //   .FirstOrDefaultAsync(a => a.Id == addressId);

        //    if (address == null)
        //    {
        //        _logger.LogWarning($"Address with {addressId} not found in database");
        //        throw new ArgumentException($"Address with {addressId} not found.");
        //    }
        //    _dbContext.Addresses.Remove(address);
        //    _logger.LogInformation($"Deleted {addressId}");            
        //}

        public async Task<IEnumerable<Person>> GetAllPersonsByUserIdAsync(int userId)
        {
            _logger.LogInformation($"Retrieving all persons for user ID: {userId} from the database");
            return await _dbContext.Persons
                .Include(p => p.Address)
                .Where(p => p.UserId == userId)
                .ToListAsync();
        }

        public async Task<Person> GetByIdAsync(int personId)
        {
            _logger.LogInformation($"Retrieving person ID: {personId} information from the database");
            return await _dbContext.Persons
                .Include(p => p.Address)
                .FirstOrDefaultAsync(p => p.Id == personId);
        }

        public async Task UpdatePersonDetailsAsync(Person person)
        {
            _logger.LogInformation($"Updating person ID: {person.Id} details in the database");
            _dbContext.Persons.Update(person);
            await _dbContext.SaveChangesAsync();
        }
    }
}
