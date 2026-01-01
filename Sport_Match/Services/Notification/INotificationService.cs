namespace Sport_Match.Services.Notification
{
    public interface INotificationService
    {
        Task SendAsync(string message);
    }
}
