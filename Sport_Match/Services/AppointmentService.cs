using Google.Apis.Calendar.v3;
using Microsoft.EntityFrameworkCore;
using Sport_Match.Data;
using Sport_Match.Models;
using Sport_Match.Services;



namespace Sport_Match.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly ICalendarService _calendarService;
        private readonly ApplicationDbContext _context;



        public AppointmentService(
    ICalendarService calendarService,
    ApplicationDbContext context)
        {
            _calendarService = calendarService;
            _context = context;
        }


        public async Task CreateAsync(Appointment appointment)
        {
            _context.Appointments.Add(appointment);
            await _context.SaveChangesAsync();
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

        public async Task<List<Appointment>> GetAllAsync()
        {
            return await _context.Appointments
                .OrderBy(a => a.StartTime)
                .ToListAsync();
        }


    }
}
