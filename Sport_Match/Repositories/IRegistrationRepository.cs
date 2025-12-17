using Sport_Match.Models;

namespace Sport_Match.Repositories
{
    public interface IRegistrationRepository
    {
        Task<Event?> GetEventByIdAsync(int eventId);
        Task<EventRegistration?> GetRegistrationAsync(int eventId, int userId);
        Task<EventRegistration?> GetNextWaitlistedAsync(int eventId);
        Task AddRegistrationAsync(EventRegistration registration);
        void RemoveRegistration(EventRegistration registration);
        Task SaveChangesAsync();
    }
}
