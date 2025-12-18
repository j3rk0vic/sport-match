using Sport_Match.Dtos;
using Sport_Match.Models;
using Sport_Match.Repositories;
using Sport_Match.Services.Security;

namespace Sport_Match.Services
{
    public class UserRegistrationService : IUserRegistrationService
    {
        private readonly IUserRepository _userRepository;

        public UserRegistrationService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> RegisterAsync(RegisterUserDto dto)
        {
            var existingUser = await _userRepository.GetByEmailAsync(dto.Email);
            if (existingUser != null)
            {
                return false;
            }

            var (hash, salt) = PasswordHasher.HashPassword(dto.Password);

            var user = new User
            {
                Email = dto.Email,
                PasswordHash = hash,
                PasswordSalt = salt
            };

            await _userRepository.AddAsync(user);
            await _userRepository.SaveChangesAsync();

            return true;
        }
    }
}
