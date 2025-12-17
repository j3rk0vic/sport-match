namespace Sport_Match.Services
{
    public interface IAppointmentService
    {
        Task SyncToGoogleAsync(string accessToken);
    }
}
