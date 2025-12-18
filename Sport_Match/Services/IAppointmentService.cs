using Sport_Match.Models;

namespace Sport_Match.Services
{
    public interface IAppointmentService
    {
        Task SyncToGoogleAsync(string accessToken);

        Task CreateAsync(Appointment appointment);

        Task<List<Appointment>> GetAllAsync();

    }
}
