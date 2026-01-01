using Sport_Match.Dtos;
using Sport_Match.Models;

namespace Sport_Match.Services.Factories
{
    public class EventFactory : IEventFactory
    {
        public Event Create(CreateEventDto dto)
        {
            return new Event
            {
                Name = dto.Name,
                Sport = dto.Sport,
                Location = dto.Location,
                IsPrivate = dto.IsPrivate,
                StartDateTime = dto.Date.Date + dto.Time,
                Capacity = 0,
                CurrentParticipants = 0
            };
        }
    }
}
