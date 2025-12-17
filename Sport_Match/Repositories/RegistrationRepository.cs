using Microsoft.EntityFrameworkCore;
using Sport_Match.Data;
using Sport_Match.Models;

namespace Sport_Match.Repositories
{
    public class RegistrationRepository : IRegistrationRepository
    {
        private readonly ApplicationDbContext _db;
        public RegistrationRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public Task<Event?> GetEventByIdAsync(int eventId)
            => _db.Events.FirstOrDefaultAsync(e => e.Id == eventId);

        public Task<EventRegistration?> GetRegistrationAsync(int eventId, int userId)
            => _db.EventRegistrations.FirstOrDefaultAsync(r => r.EventId == eventId && r.UserId == userId);

        public Task<EventRegistration?> GetNextWaitlistedAsync(int eventId)
            => _db.EventRegistrations
                .Where(r => r.EventId == eventId && !r.IsConfirmed)
                .OrderBy(r => r.CreatedAt)
                .FirstOrDefaultAsync();
        public Task AddRegistrationAsync(EventRegistration registration)
            => _db.EventRegistrations.AddAsync(registration).AsTask();

        public void RemoveRegistration(EventRegistration registration)
            => _db.EventRegistrations.Remove(registration);

        public Task SaveChangesAsync()
            => _db.SaveChangesAsync();
    }
}
