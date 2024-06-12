using Microsoft.AspNetCore.Mvc;
using Exam.BusinessLogic.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Exam.Shared.DTOs;
using System.Security.Claims;
using Exam.BusinessLogic.Services;

namespace Exam.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin,User")]
    public class PersonController : ControllerBase
    {
        private readonly IPersonService _personService;
        private readonly ILogger<PersonController> _logger;

        public PersonController(IPersonService personService, ILogger<PersonController> logger)
        {
            _logger = logger;
            _personService = personService;
        }

        [HttpPost("Add")]
        [ProducesResponseType(200)] //Ok
        [ProducesResponseType(400)] //Bad Request
        [ProducesResponseType(401)] //Unauthorized
        public async Task<IActionResult> Create([FromForm] PersonCreateDTO personCreateDTO)
        {
            int? userId = GetUserIdFromClaims();
            if (userId == null)
            {
                return Unauthorized();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var person = await _personService.CreatePersonAsync((int)userId, personCreateDTO);
                return Ok($"{person.Name} {person.LastName} added successfully");
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, $"Error creating person: {personCreateDTO.LastName}");
                return BadRequest(exception.Message);
            }

        }

        [HttpPut("Update{id}")]
        [ProducesResponseType(200)] //Ok
        [ProducesResponseType(400)] //Bad Request
        [ProducesResponseType(401)] //Unauthorized


        public async Task<IActionResult> UpdatePersonDetails(int id, [FromForm] PersonUpdateDTO personUpdateDTO)
        {
            int? userId = GetUserIdFromClaims();
            if (userId == null)
            {
                return Unauthorized();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var updatedPerson = await _personService.UpdatePersonAsync(userId.Value, id, personUpdateDTO);
                return Ok(updatedPerson);
            }
            catch (ArgumentException exception)
            {
                return Unauthorized(exception.Message);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, $"An error occurred while updating person ID {id} details.");
                return BadRequest(exception.Message);
            }
        }

        [HttpGet("GetByID{id}")]
        [ProducesResponseType(200)] //Ok
        [ProducesResponseType(400)] //Bad Request
        [ProducesResponseType(403)]//Forbid
        public async Task<IActionResult> GetPersonById(int id)
        {
            int? userId = GetUserIdFromClaims();
            if (userId == null)
            {
                return Unauthorized();
            }

            try
            {
                var person = await _personService.GetByIdAsync(userId.Value, id);
                return Ok(person);
            }

            catch (Exception exception)
            {
                _logger.LogError(exception, $"An error occurred: {exception.Message}");
                return BadRequest(exception.Message);
            }
        }

        [HttpDelete("Delete{id}")]
        [ProducesResponseType(200)] //Ok
        [ProducesResponseType(404)] //Not Found
        [ProducesResponseType(400)] //Bad Request
        public async Task<IActionResult> DeletePerson(int id)
        {
            int? userId = GetUserIdFromClaims();
            if (userId == null)
            {
                return Unauthorized();
            }

            try
            {
                var deletedPerson = await _personService.DeletePersonAsync(id, (int)userId);
                if (deletedPerson == null)
                {
                    return NotFound("Person not found.");
                }
                return Ok(deletedPerson);
            }

            catch (KeyNotFoundException exception)
            {
                return NotFound(exception.Message);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, $"An error occurred while deleting the person with ID: {id}");
                return BadRequest(exception.Message);
            }
        }
        private int? GetUserIdFromClaims()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                _logger.LogWarning("User ID claim not found.");
                foreach (var claim in User.Claims)
                {
                    _logger.LogInformation($"Claim type: {claim.Type}, value: {claim.Value}");
                }
                return null;
            }
            return int.Parse(userIdClaim.Value);
        }


    }
}
