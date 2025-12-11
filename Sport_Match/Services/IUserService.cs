using Sport_Match.Dtos;
using Sport_Match.Models;

namespace Sport_Match.Services
{
    public interface IUserService
    {
        Task<bool> RegisterAsync(RegisterUserDto dto);

        Task<User?> AutenticateAsync(LoginUserDto dto);
    }
}
