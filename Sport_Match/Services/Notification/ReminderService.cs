using Sport_Match.Models;

namespace Sport_Match.Services.Notification
{
    public class ReminderService : IReminderService
    {
        private readonly INotificationStrategy _strategy;

        public ReminderService(INotificationStrategy strategy)
        {
            _strategy = strategy;
        }

        public async Task ScheduleAsync(Appointment appointment)
        {
            var reminderTime = appointment.StartTime.AddHours(-24);

            Console.WriteLine(
                $"Reminder scheduled for {reminderTime:dd.MM.yyyy HH:mm}");

            var message =
                $"Podsjetnik: Termin '{appointment.Title}' " +
                $"na lokaciji {appointment.Location} počinje " +
                $"{appointment.StartTime:dd.MM.yyyy HH:mm}";

            await _strategy.NotifyAsync(message);
        }
    }
}
