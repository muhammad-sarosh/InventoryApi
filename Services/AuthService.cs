using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace InventoryApi.Services
{
    public class AuthService
    {
        private readonly InventoryDbContext _dbContext;
        private readonly PasswordHasher<User> _passwordHasher = new();

        public AuthService(InventoryDbContext dbContext)
        {
            _dbContext = dbContext;
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

        public bool Login(LoginUserDto loginUserDto)
        {
            var user = _dbContext.Users.FirstOrDefault(u => u.Username == loginUserDto.Username);

            if (user == null)
            {
                return false;
            }

            var result = _passwordHasher.VerifyHashedPassword(
                    user,
                    user.PasswordHash,
                    loginUserDto.Password
            );

            return result == PasswordVerificationResult.Success;
        }
    }
}
