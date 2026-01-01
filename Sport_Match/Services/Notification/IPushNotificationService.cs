namespace Sport_Match.Services.Notification
{
    public interface IPushNotificationService
    {
        Task SendAsync(string message);
    }
}
