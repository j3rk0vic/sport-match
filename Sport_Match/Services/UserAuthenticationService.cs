using Sport_Match.Dtos;
using Sport_Match.Models;
using Sport_Match.Repositories;
using Sport_Match.Services.Security;

namespace Sport_Match.Services
{
    public class UserAuthenticationService : IUserAuthenticationService
    {
        private readonly IUserRepository _userRepository;

        public UserAuthenticationService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User?> AuthenticateAsync(LoginUserDto dto)
        {
            var user = await _userRepository.GetByEmailAsync(dto.Email);
            if (user == null)
            {
                return null;
            }

            var isPasswordValid = PasswordHasher.VerifyPassword(
                dto.Password,
                user.PasswordHash,
                user.PasswordSalt);

            if (!isPasswordValid)
            {
                return null;
            }

            return user;
        }
    }
}
