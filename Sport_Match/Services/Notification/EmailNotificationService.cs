namespace Sport_Match.Services.Notification
{
    public class EmailNotificationService : INotificationService
    {
        public Task SendAsync(string message)
        {
            Console.WriteLine($"[EMAIL] {message}");
            return Task.CompletedTask;
        }
    }
}
