using Sport_Match.Dtos;
using Sport_Match.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sport_Match.Services.Facades
{
    public interface IEventFacade
    {
        Task<IEnumerable<Event>> SearchAsync(EventSearchRequest req);
        Task<Event> GetByIdAsync(int id);
        Task<Event> CreateAsync(CreateEventDto dto);
    }
}
