namespace Sport_Match.Services.Notification
{
    public class EmailAndPushStrategy : INotificationStrategy
    {
        private readonly INotificationService _email;
        private readonly INotificationService _push;

        public EmailAndPushStrategy()
        {
            _email = new LoggingNotificationDecorator(
                NotificationFactory.Create("email"));

            _push = new LoggingNotificationDecorator(
                NotificationFactory.Create("push"));
        }

        public async Task NotifyAsync(string message)
        {
            await _email.SendAsync(message);
            await _push.SendAsync(message);
        }
    }

}
