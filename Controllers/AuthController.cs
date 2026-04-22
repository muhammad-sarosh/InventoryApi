using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using InventoryApi.Services;

namespace InventoryApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;
        
        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterUserDto registerUserDto)
        {
            if (string.IsNullOrWhiteSpace(registerUserDto.Username) ||
                string.IsNullOrWhiteSpace(registerUserDto.Password))
            {
                return BadRequest("Username and password are required.");
            }

            bool registered = _authService.RegisterUser(registerUserDto);

            if (!registered)
            {
                return BadRequest("User already registered.");
            }

            return Ok("User registered successfully.");
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginUserDto loginUserDto)
        {
            if (string.IsNullOrWhiteSpace(loginUserDto.Username) ||
                string.IsNullOrWhiteSpace(loginUserDto.Password))
            {
                return BadRequest("Username and password are required.");
            }

            var token = _authService.Login(loginUserDto);

            if (token == null)
            {
                return Unauthorized("Invalid username or password.");
            }

            return Ok(new { token });
        }

        [Authorize]
        [HttpGet("me")]
        public IActionResult Me()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var username = User.FindFirst(ClaimTypes.Name)?.Value;
            var role = User.FindFirst(ClaimTypes.Role)?.Value;

            return Ok(new
            {
                Id = userId,
                Username = username,
                Role = role
            });
        }
    }
}
