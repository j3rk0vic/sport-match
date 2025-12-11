using Sport_Match.Dtos;
using Sport_Match.Models;
using Sport_Match.Dtos;

using System.Threading.Tasks;

namespace Sport_Match.Services
{
    public interface IEventService
    {
        Task<Event> CreateEventAsync(CreateEventDto dto);
        Task<string?> GetByIdAsync(int id);
        Task<IEnumerable<Event>> SearchEventsAsync(EventSearchRequest req);

    }
}
