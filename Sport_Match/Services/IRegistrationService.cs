namespace Sport_Match.Services
{
    public interface IRegistrationService
    {
        Task<string> RegisterAsync(int eventId, int userId);
        Task RegisterAsync(int eventId, object userId);
        Task<string> UnregisterAsync(int eventId, int userId);
    }
}
