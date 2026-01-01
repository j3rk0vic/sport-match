namespace Sport_Match.Services.Notification
{
    public class EmailOnlyStrategy : INotificationStrategy
    {
        private readonly INotificationService _email;

        public EmailOnlyStrategy()
        {
            _email = new LoggingNotificationDecorator(
                NotificationFactory.Create("email"));
        }

        public Task NotifyAsync(string message)
            => _email.SendAsync(message);
    }

}
