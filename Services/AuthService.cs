using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace InventoryApi.Services
{
    public class AuthService
    {
        private readonly InventoryDbContext _dbContext;
        private readonly PasswordHasher<User> _passwordHasher = new();
        private readonly IConfiguration _configuration;

        public AuthService(InventoryDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _configuration = configuration;
        }

        private string GenerateJwtToken(User user)
        {
            var claims = new[]
            {
        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
        new Claim(ClaimTypes.Name, user.Username),
        new Claim(ClaimTypes.Role, user.Role)
    };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["Jwt:Key"])
            );

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public bool RegisterUser(RegisterUserDto registerUserDto)
        {
            bool userExists = _dbContext.Users.Any(u => u.Username == registerUserDto.Username);

            if (userExists)
            {
                return false;
            }

            var user = new User
            {
                Username = registerUserDto.Username,
                Role = "User"
            };

            user.PasswordHash = _passwordHasher.HashPassword(user, registerUserDto.Password);

            _dbContext.Add(user);
            _dbContext.SaveChanges();

            return true;
        }

        public string? Login(LoginUserDto loginUserDto)
        {
            var user = _dbContext.Users.FirstOrDefault(u => u.Username.ToLower() == loginUserDto.Username.Trim().ToLower());

            if (user == null)
            {
                return null;
            }

            var result = _passwordHasher.VerifyHashedPassword(
                    user,
                    user.PasswordHash,
                    loginUserDto.Password
            );

            if (result == PasswordVerificationResult.Failed)
            {
                return null;
            }

            return GenerateJwtToken(user);
        }
    }
}
