namespace Sport_Match.Services
{
    public interface IRegistrationService
    {
        Task<string> RegisterAsync(int eventId, int userId);
        Task<string> UnregisterAsync(int eventId, int userId);
    }
}
