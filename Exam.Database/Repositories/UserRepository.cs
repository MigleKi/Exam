using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Exam.Database.Models;
using Exam.Database.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Exam.Database.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly MainDbContext _dbContext;
        private readonly ILogger<UserRepository> _logger;

        public UserRepository(MainDbContext dbContext, ILogger<UserRepository> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task CreateAsync(User user)
        {
            _logger.LogInformation($"Adding new user: {user.Username} to the database");
            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();
            // _logger.LogInformation($"User: {user.Username} added successfully");
        }

        public async Task<User> DeleteAsync(int userId)
        {
            var user = await _dbContext.Users
                 .Include(u => u.Persons)
                 .ThenInclude(p => p.Address)
                 .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
            {
                _logger.LogWarning($"User with {userId} not found in database");
                throw new ArgumentException($"User with {userId} not found.");
            }

            foreach (var person in user.Persons)
            {
                _logger.LogInformation($"Deleting person ID: {person.Id} for user ID: {userId}");

                //if (!string.IsNullOrEmpty(person.ProfilePhotoPath) && File.Exists(person.ProfilePhotoPath))
                //{
                //    File.Delete(person.ProfilePhotoPath);
                //    _logger.LogInformation($"Person {person.Id} picture {person.ProfilePhoto} has been removed from the database.");
                //}

                var address = await _dbContext.Addresses.FirstOrDefaultAsync(p => p.PersonId == person.Id);
                if (address != null)
                {
                    _dbContext.Addresses.Remove(address);
                    _logger.LogInformation($"Person {person.Id} address {person.AddressId} has been removed from the database.");
                }

                _dbContext.Persons.Remove(person);
                _logger.LogInformation($"Person with ID: {person.Id} has been removed from the database.");
            }

            _dbContext.Users.Remove(user);
            _logger.LogInformation($"User with {userId} has been removed from database");
            await _dbContext.SaveChangesAsync();

            return user;
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            _logger.LogInformation("Getting all users from database");
            return await _dbContext.Users.ToListAsync();
        }

        public async Task<User> GetByIdAsync(int userId)
        {
            _logger.LogInformation($"Getting user information by ID: {userId}");
            return await _dbContext.Users
                .Include(u => u.Persons)
                .FirstOrDefaultAsync(u => u.Id == userId);
        }

        public async Task<User> GetByUsernameAsync(string username)
        {
            _logger.LogInformation($"Getting user information by username: {username}");
            return await _dbContext.Users.FirstOrDefaultAsync(u => u.Username == username);
        }
    }
}
