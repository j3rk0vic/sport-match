using Sport_Match.Models;

namespace Sport_Match.Services.Notification
{
    public interface IReminderService
    {
        Task ScheduleAsync(Appointment appointment);
    }
}
