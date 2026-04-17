using InventoryApi.Services;
using Microsoft.AspNetCore.Mvc;

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

            bool loggedIn = _authService.Login(loginUserDto);

            if (!loggedIn)
            {
                return Unauthorized("Invalid username or password.");
            }

            return Ok("Login successful.");
        }
    }
}
