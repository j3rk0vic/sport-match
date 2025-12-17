using Sport_Match.Models;

namespace Sport_Match.Services
{
    public interface ICalendarService
    {
        Task<string> CreateEventAsync(string accessToken, Appointment appointment);
    }
}
