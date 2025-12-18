using Sport_Match.Dtos;

namespace Sport_Match.Services
{
    public interface IUserRegistrationService
    {
        Task<bool> RegisterAsync(RegisterUserDto dto);
    }
}
