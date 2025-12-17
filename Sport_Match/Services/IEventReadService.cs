using Sport_Match.Dtos;
using Sport_Match.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sport_Match.Services
{
    public interface IEventReadService
    {
        Task<Event> GetByIdAsync(int id);
        Task<IEnumerable<Event>> SearchEventsAsync(EventSearchRequest req);
    }
}
