using System.Threading.Tasks;
using Sport_Match.Dtos;
using Sport_Match.Models;
using Sport_Match.Repositories;
using Sport_Match.Services.Security;

namespace Sport_Match.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;

        public UserService(IUserRepository userRepository, IPasswordHasher passwordHasher)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
        }

        public async Task<bool> RegisterAsync(RegisterUserDto dto)
        {
            var existingUser = await _userRepository.GetByEmailAsync(dto.Email);
            if (existingUser != null)
            {
                return false;
            }

            var (hash, salt) = _passwordHasher.HashPassword(dto.Password);

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

        public async Task<User?> AutenticateAsync(LoginUserDto dto)
        {
            var user = await _userRepository.GetByEmailAsync(dto.Email);
            if (user == null)
            {
                return null;
            }

            bool passwordValid = _passwordHasher.VerifyPassword(dto.Password, user.PasswordHash, user.PasswordSalt);


            if (!passwordValid)
            {
                return null;
            }

            return user;
        }
    }
}