namespace Sport_Match.Services.Notification
{
    public class PushNotificationService : INotificationService
    {
        public Task SendAsync(string message)
        {
            Console.WriteLine($"[PUSH] {message}");
            return Task.CompletedTask;
        }
    }
}
