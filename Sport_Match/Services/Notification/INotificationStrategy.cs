namespace Sport_Match.Services.Notification
{
    public interface INotificationStrategy
    {
        Task NotifyAsync(string message);
    }
}
