
using Microsoft.AspNetCore.Mvc;
using Exam.BusinessLogic.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Exam.Shared.DTOs;


namespace Exam.Controllers
{

    [Route("api/[controller]")]
    [ApiController]

    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger<UsersController> _logger;


        public UsersController(IUserService userService, ILogger<UsersController> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        [HttpPost("Register")]
        [AllowAnonymous]
        [ProducesResponseType(200)] //Ok
        [ProducesResponseType(400)] //Bad Request
        public async Task<IActionResult> Register([FromBody] UserRegisterDTO userRegisterDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var user = await _userService.RegisterUserAsync(userRegisterDTO);
                return Ok($"{user.Username} registered successfully, you can now login.");
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, $"Error registering user: {userRegisterDTO.Username}");
                return BadRequest(exception.Message);
            }
        }

        [HttpPost("Login")]
        [AllowAnonymous]
        [ProducesResponseType(200)] //Ok
        [ProducesResponseType(400)]//Bad Request
        [ProducesResponseType(401)] //Unauthorized
        public async Task<IActionResult> Login(UserLoginDTO userLoginDTO)
        {
            _logger.LogInformation($"Logging in user: {userLoginDTO.Username}");
            try
            {
                var tokenDTO = await _userService.LoginUserAsync(userLoginDTO);
                return Ok(tokenDTO);
            }
            catch (UnauthorizedAccessException exception)
            {
                _logger.LogError(exception, $"Error logging in user: {userLoginDTO.Username}");
                return Unauthorized(exception.Message);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, $"Error logging in user: {userLoginDTO.Username}");
                return BadRequest(exception.Message);
            }
        }

        [HttpGet("GetAllUsers")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(200)] //Ok
        [ProducesResponseType(400)] //Bad Request
        [ProducesResponseType(403)]//Forbid
        public async Task<IActionResult> GetAllUsers()
        {

            _logger.LogInformation("Getting all users");
            try
            {
                var users = await _userService.GetAllUsersAsync();
                return Ok(users);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "Error getting all users");
                return BadRequest(exception.Message);
            }
        }

        [HttpDelete("Delete{id}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(200)] //Ok
        [ProducesResponseType(403)]//Forbid
        [ProducesResponseType(404)] //Not Found
        [ProducesResponseType(400)] //Bad Request

        public async Task<IActionResult> DeleteUser(int id)
        {

            try
            {
                var deletedUser = await _userService.DeleteUserAsync(id);
                if (deletedUser == null)
                {
                    return NotFound("User not found.");
                }
                return Ok(deletedUser);
            }
            catch (UnauthorizedAccessException exception)
            {
                return Forbid(exception.Message);
            }
            catch (KeyNotFoundException exception)
            {
                return NotFound(exception.Message);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, $"An error occurred while deleting the user with ID: {id}");
                return BadRequest(exception.Message);
            }
        }
    }
}
