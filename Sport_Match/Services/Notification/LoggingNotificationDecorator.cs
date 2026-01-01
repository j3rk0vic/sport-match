namespace Sport_Match.Services.Notification
{
    public class LoggingNotificationDecorator : INotificationService
    {
        private readonly INotificationService _inner;

        public LoggingNotificationDecorator(INotificationService inner)
        {
            _inner = inner;
        }

        public async Task SendAsync(string message)
        {
            Console.WriteLine("[LOG] Slanje notifikacije...");
            await _inner.SendAsync(message);
            Console.WriteLine("[LOG] Notifikacija poslana.");
        }
    }
}
