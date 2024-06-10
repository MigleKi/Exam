using Exam.BusinessLogic.Services.Interfaces;
using Exam.Database.Models;
using Exam.Shared.DTOs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.Collections;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Exam.BusinessLogic.Services
{
    public class JWTService : IJWTService
    {

        private readonly IConfiguration _configuration;
        private readonly ILogger<JWTService> _logger;
        public JWTService(IConfiguration configuration, ILogger<JWTService> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }
        public string GetJWT(string user, string role)
        {
            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, user),
               new Claim(ClaimTypes.Role, role)
        };
            var secretToken = _configuration.GetSection("JWT:Key").Value;
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(secretToken));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

            var token = new JwtSecurityToken(
                issuer: _configuration.GetSection("JWT:Issuer").Value,
                audience: _configuration.GetSection("JWT:Audience").Value,
                claims: claims,
                expires: DateTime.Now.AddMinutes(10),
                signingCredentials: cred);
            return new JwtSecurityTokenHandler().WriteToken(token);

        }


        public bool VerifyPassword(string password, byte[] storedHash, byte[] storedSalt)
        {
            using var hmac = new HMACSHA512(storedSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            return StructuralComparisons.StructuralEqualityComparer.Equals(computedHash, storedHash);
        }

        public User CreateUser(string username, string password)
        {
            _logger.LogInformation($"Generating token for user: {username}");
            CreatePasswordHash(password, out var passwordHash, out var passwordSalt);
            return new User
            {
                Username = username,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
            };

        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using var hmac = new HMACSHA512();
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        }

    }
}
