using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DotNetEnv;

namespace Backend.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {   

        private readonly ILogger<AuthController> _logger;

        // Inject ILogger into the constructor
        public AuthController(ILogger<AuthController> logger)
        {
            _logger = logger;
        }

        // POST: api/auth/login
        [HttpPost("login")]
        public IActionResult Login([FromBody] UserLogin loginRequest)
        {
            if (loginRequest == null)
            {
                Console.WriteLine("Invalid login attempt");
                return BadRequest("Invalid login request");
            }

            string username = loginRequest.Username;
            string password = loginRequest.Password;


            Console.WriteLine("Login successful");

            //generate JWT token
            string token = GenerateJwtToken(username);

            return Ok(new { Token = token });
        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {

            _logger.LogInformation("User logged out successfully.");
            return Ok(new { message = "Logout successful" });
            
        }

        public string GenerateJwtToken(string username)
        {
            //secret key
            var secretKey = Environment.GetEnvironmentVariable("JWT_KEY");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

            //signing credentials
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // claims (data inside the JWT)
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, username)
            };

            // Creating the token
            var tokenDescriptor = new JwtSecurityToken(
                issuer: "PatientCareManager",  // API issuer
                audience: "frontend",  // API audience
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),  // sets expiration time for the token
                signingCredentials: credentials
            );

            // Generate the JWT token
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.WriteToken(tokenDescriptor);

            return token;
        }
    }

    // UserLogin class for capturing login request
    public class UserLogin
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
