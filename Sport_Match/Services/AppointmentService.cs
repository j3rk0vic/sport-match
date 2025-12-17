using Google.Apis.Calendar.v3;
using Microsoft.EntityFrameworkCore;
using Sport_Match.Data;
using Sport_Match.Services;



namespace Sport_Match.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly ICalendarService _calendarService;
        private readonly ApplicationDbContext _context;
       


        public AppointmentService(ICalendarService calendarService)
        {
            _calendarService = calendarService;
        }

        public async Task SyncToGoogleAsync(string accessToken)
        {
            var appointment = await _context.Appointments
                .OrderByDescending(a => a.StartTime)
                .FirstOrDefaultAsync();

            if (appointment == null)
                return;

            await _calendarService.CreateEventAsync(accessToken, appointment);
        }

    }
}
