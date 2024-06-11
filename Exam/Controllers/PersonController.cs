using Microsoft.AspNetCore.Mvc;
using Exam.BusinessLogic.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Exam.Shared.DTOs;
using Exam.BusinessLogic.Services;
using System.Security.Claims;
using System.Data;

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
