using Sport_Match.Dtos;
using Sport_Match.Models;

namespace Sport_Match.Services
{
    public interface IUserAuthenticationService
    {
        Task<User?> AuthenticateAsync(LoginUserDto dto);
    }
}
