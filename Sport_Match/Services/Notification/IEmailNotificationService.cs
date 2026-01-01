namespace Sport_Match.Services.Notification
{
    public interface IEmailNotificationService
    {
        Task SendAsync(string message);
    }
}
