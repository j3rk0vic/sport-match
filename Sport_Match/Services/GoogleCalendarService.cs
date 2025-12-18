using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;
using Sport_Match.Models;

namespace Sport_Match.Services
{
    public class GoogleCalendarService : ICalendarService
    {
        public async Task<string> CreateEventAsync(string accessToken, Appointment appointment)
        {
            var credential = GoogleCredential.FromAccessToken(accessToken);

            var service = new CalendarService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = "Sport Match"
            });

            var newEvent = new Google.Apis.Calendar.v3.Data.Event
            {
                Summary = appointment.Title,
                Location = appointment.Location,
                Description = appointment.Notes,
                Start = new EventDateTime
                {
                    DateTime = appointment.StartTime,
                    TimeZone = "Europe/Zagreb"
                },
                End = new EventDateTime
                {
                    DateTime = appointment.EndTime,
                    TimeZone = "Europe/Zagreb"
                }
            };

            var request = service.Events.Insert(newEvent, "primary");
            var createdEvent = await request.ExecuteAsync();

            return createdEvent.Id;
        }
    }
}
