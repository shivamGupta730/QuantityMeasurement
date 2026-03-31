using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Context;
using ModelLayer.Entity;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using QuantityMeasurementAPI.DTOs;
using BCrypt.Net;
using System.Text;
using Google.Apis.Auth;
using Microsoft.EntityFrameworkCore;

namespace QuantityMeasurementAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly ILogger<AuthController> _logger;

public AuthController(AppDbContext context, IConfiguration configuration, ILogger<AuthController> logger)
        {
            _context = context;
            _configuration = configuration;
            _logger = logger;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto request)
        {
            try
            {
                _logger.LogInformation("Register attempt for email: {Email}", request.Email);

                if (await _context.Users.AnyAsync(u => u.Email == request.Email))
                {
                    return BadRequest("User already exists");
                }

                _logger.LogInformation("Hashing password for new user {Email}", request.Email);
                var passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);
                var user = new User
                {
                    Email = request.Email,
                    PasswordHash = passwordHash,
                    CreatedAt = DateTime.UtcNow
                };

                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                _logger.LogInformation("User registered successfully: {Id}", user.Id);

                return Ok(new { message = "User registered successfully" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Register failed for email: {Email}", request.Email);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto request)
        {
            try
            {
                _logger.LogInformation("Login attempt for email: {Email}", request.Email);

                var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == request.Email);
                if (user == null)
                {
                    _logger.LogWarning("Login failed: User not found for email {Email}", request.Email);
                    return Unauthorized("Invalid credentials");
                }

                bool isPasswordValid = BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash);
                _logger.LogInformation("Password verification for user {Email}: {IsValid}", request.Email, isPasswordValid);

                if (!isPasswordValid)
                {
                    return Unauthorized("Invalid credentials");
                }

                var token = GenerateJwtToken(user);
                var response = new AuthResponseDto
                {
                    Token = token,
                    Expiration = DateTime.UtcNow.AddHours(1)
                };

                _logger.LogInformation("User logged in successfully: {Id}", user.Id);

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Login failed for email: {Email}", request.Email);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost("google-login")]
        public async Task<IActionResult> GoogleLogin([FromBody] GoogleLoginDto request)
        {
            try
            {
                _logger.LogInformation("Google login attempt for id_token length: {Length}", request.IdToken.Length);

                var clientId = _configuration["Google:ClientId"];
                if (string.IsNullOrEmpty(clientId))
                {
                    return BadRequest("Google ClientId not configured");
                }

                var payload = await GoogleJsonWebSignature.ValidateAsync(request.IdToken, new GoogleJsonWebSignature.ValidationSettings
                {
                    Audience = new[] { clientId }
                });

                var email = payload.Email;
                var name = payload.Name ?? "";
                var googleSub = payload.Subject;

                _logger.LogInformation("Validated Google token for email: {Email}", email);

                var appUser = await _context.ApplicationUsers.FirstOrDefaultAsync(u => u.Email == email);
                if (appUser == null)
                {
                    _logger.LogInformation("Creating new ApplicationUser for Google: {Email}", email);
                    appUser = new ApplicationUser
                    {
                        Id = googleSub,
                        Email = email,
                        FullName = name,
                        CreatedAt = DateTime.UtcNow
                    };
                    _context.ApplicationUsers.Add(appUser);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    _logger.LogInformation("Existing ApplicationUser found for Google: {Email}", email);
                }

                var token = GenerateJwtToken(appUser);
                var response = new AuthResponseDto
                {
                    Token = token,
                    Expiration = DateTime.UtcNow.AddHours(1)
                };

                _logger.LogInformation("Google login successful for: {Email}", email);

                return Ok(response);
            }
            catch (InvalidJwtException ex)
            {
                _logger.LogWarning("Invalid Google ID token: {Message}", ex.Message);
                return Unauthorized("Invalid Google token");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Google login failed");
                return StatusCode(500, "Internal server error");
            }
        }

        private string GenerateJwtToken(ApplicationUser appUser)
        {
            var jwtKey = _configuration["Jwt:Key"];
            var issuer = _configuration["Jwt:Issuer"];
            var audience = _configuration["Jwt:Audience"];

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, appUser.Id),
                new Claim(ClaimTypes.Email, appUser.Email),
                new Claim("userType", "external")
            };

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private string GenerateJwtToken(User user)
        {
            var jwtKey = _configuration["Jwt:Key"];
            var issuer = _configuration["Jwt:Issuer"];
            var audience = _configuration["Jwt:Audience"];

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email)
            };

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
