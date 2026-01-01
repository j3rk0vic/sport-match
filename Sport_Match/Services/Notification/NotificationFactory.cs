namespace Sport_Match.Services.Notification
{
    public static class NotificationFactory
    {
        public static INotificationService Create(string type)
        {
            return type switch
            {
                "email" => new EmailNotificationService(),
                "push" => new PushNotificationService(),
                _ => throw new ArgumentException("Nepoznat tip notifikacije")
            };
        }
    }
}
