using AutoMapper;
using Exam.BusinessLogic.Services.Interfaces;
using Exam.Database.Models;
using Exam.Database.Repositories;
using Exam.Database.Repositories.Interfaces;
using Exam.Shared.DTOs;
using Microsoft.Extensions.Logging;


namespace Exam.BusinessLogic.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IJWTService _jwtService;
        private readonly ILogger<UserService> _logger;
        private readonly IMapper _mapper;
        //input DTO ir return DTO -> mapinasi servise
        public UserService(IUserRepository userRepository, IJWTService jwtService, ILogger<UserService> logger, IMapper mapper)
        {
            _userRepository = userRepository;
            _jwtService = jwtService;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task<UserRegisterDTO> RegisterUserAsync(UserRegisterDTO userRegisterDTO)
        {
            _logger.LogInformation($"Registration for user: {userRegisterDTO.Username} started");

            var existingUser = await _userRepository.GetByUsernameAsync(userRegisterDTO.Username);
            if (existingUser != null)
            {
                _logger.LogWarning($"Username {userRegisterDTO.Username} already exists.");
                throw new ArgumentException("Username already exists.");
            }
            var user = _mapper.Map<User>(userRegisterDTO);

            user = _jwtService.CreateUser(userRegisterDTO.Username, userRegisterDTO.Password);
            user.Role = "User";
            await _userRepository.CreateAsync(user);

            _logger.LogInformation($"User {userRegisterDTO.Username} registered successfully.");
            return _mapper.Map<UserRegisterDTO>(user);

        }

        public async Task<UserDeleteDTO> DeleteUserAsync(int userId)
        {
            _logger.LogInformation($"Deletion for user ID: {userId} started");

            var existingUser = await _userRepository.GetByIdAsync(userId);
            if (existingUser == null)
            {
                _logger.LogWarning($"User with ID: {userId} not found");
                throw new ArgumentException("User not found");
            }

            //foreach (var person in existingUser.Persons)
            //{
            //    _logger.LogInformation($"Deleting person with ID: {person.Id} for user ID: {userId}");
            //    await _personRepository.DeleteAsync(person.Id);
            //}

            var deletedUser = await _userRepository.DeleteAsync(userId);
            return _mapper.Map<UserDeleteDTO>(deletedUser);
        }

        public async Task<IEnumerable<UserGetAllDTO>> GetAllUsersAsync()
        {
            var allUsers = await _userRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<UserGetAllDTO>>(allUsers);
        }

        public async Task<string> LoginUserAsync(UserLoginDTO userLoginDTO)
        {

            if (string.IsNullOrWhiteSpace(userLoginDTO.Username) || string.IsNullOrWhiteSpace(userLoginDTO.Password))
            {
                throw new ArgumentException("Username and password must be provided.");
            }

            _logger.LogInformation($"Logging in user with username: {userLoginDTO.Username}");

            var existingUser = await _userRepository.GetByUsernameAsync(userLoginDTO.Username);

            if (existingUser == null)
            {
                _logger.LogWarning($"User with username: {userLoginDTO.Username} not found");
                throw new UnauthorizedAccessException("Invalid username and/or password");
            }
            else if (!_jwtService.VerifyPassword(userLoginDTO.Password, existingUser.PasswordHash, existingUser.PasswordSalt))
            {
                _logger.LogWarning($"Invalid password entered for username:{userLoginDTO.Username}");
                throw new UnauthorizedAccessException("Invalid username and/or password");
            }


            var token = _jwtService.GetJWT(existingUser.Username, existingUser.Role);

            _logger.LogInformation($"User {userLoginDTO.Username} logged in successfully.");
            return token;
        }


    }
}
