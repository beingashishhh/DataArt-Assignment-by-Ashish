using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MyProject.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _config;

        public AuthController(IConfiguration config)
        {
            _config = config;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Username) || string.IsNullOrWhiteSpace(request.Password))
                return BadRequest("Username and password are required.");

            // 🔑 Demo credentials (change later to DB check)
            if (request.Username == "admin" && request.Password == "password")
            {
                var token = GenerateJwtToken(request.Username);
                return Ok(new { token }); // lowercase property = easier for Swagger/Postman
            }

            return Unauthorized();
        }

        private string GenerateJwtToken(string username)
        {
            var jwtKey = _config["Jwt:Key"] ?? throw new InvalidOperationException("JWT Key is missing");
            var jwtIssuer = _config["Jwt:Issuer"] ?? "MyProject";
            var jwtAudience = _config["Jwt:Audience"] ?? "MyProjectUsers";
            var expiryMinutes = int.TryParse(_config["Jwt:ExpiryMinutes"], out var mins) ? mins : 60;

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: jwtIssuer,
                audience: jwtAudience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(expiryMinutes),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }

    public class LoginRequest
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
