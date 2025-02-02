using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DotNetEnv;
using Backend.Data;
using BCrypt.Net;
using Microsoft.EntityFrameworkCore;
using System; 
using System.Linq; 
using Microsoft.AspNetCore.Mvc;


namespace Backend.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {   

        private readonly ILogger<AuthController> _logger;
        private readonly AppDbContext _context;
        private readonly IConfiguration _config;

         public AuthController(AppDbContext context, IConfiguration config, ILogger<AuthController> logger)
        {
            _context = context;
            _config = config;
            _logger = logger;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(User request)
        {
            if (await _context.Users.AnyAsync(u => u.Email == request.Email))
            {
                return BadRequest("User already exists.");
            }
    
            var passwordSalt = BCrypt.Net.BCrypt.GenerateSalt();
            var passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password, passwordSalt);

            var user = new User
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Gender = request.Gender,
                Email = request.Email,
                DateOfBirth = request.DateOfBirth,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Role = request.Role,
                AccountStatus = request.AccountStatus,
            };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok("User registered successfully.");
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLogin loginRequest)
        {
            if (loginRequest == null)
            {
                _logger.LogWarning("Invalid login attempt: null request");
                return BadRequest("Invalid login request");
            }

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == loginRequest.Email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(loginRequest.Password, user.PasswordHash))
            {
                _logger.LogWarning($"Invalid login attempt for user: {loginRequest.Email}");
                return Unauthorized("Invalid credentials");
            }
            _logger.LogInformation($"Login successful for user: {user.Email}");

            // Generate JWT token
            string token = GenerateJwtToken(user);

          return Ok(new 
            {
                Token = token,
                User = new
                {
                    user.FirstName,
                    user.LastName,
                    user.Email,
                    user.Role
                }
            });
        }


        [HttpPost("logout")]
        public IActionResult Logout()
        {

            _logger.LogInformation("User logged out successfully.");
            return Ok(new { message = "Logout successful" });
            
        }


        private string GetJwtSecret()
        {
            var secretKey = Environment.GetEnvironmentVariable("JWT_KEY");
            if (string.IsNullOrEmpty(secretKey))
            {
                throw new Exception("JWT_KEY is not set in the environment variables.");
            }
            return secretKey;
        }

        private string GenerateJwtToken(User user)
        {
           
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(GetJwtSecret()));

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(ClaimTypes.Role, user.Role) //RBAC
            };

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
        }
    }

    // UserLogin class for capturing login request
    public class UserLogin
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
