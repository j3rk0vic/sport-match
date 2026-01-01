using Sport_Match.Dtos;
using Sport_Match.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sport_Match.Services.Facades
{
    public class EventFacade : IEventFacade
    {
        private readonly IEventReadService _readService;
        private readonly IEventWriteService _writeService;

        public EventFacade(
            IEventReadService readService,
            IEventWriteService writeService)
        {
            _readService = readService;
            _writeService = writeService;
        }

        public Task<IEnumerable<Event>> SearchAsync(EventSearchRequest req)
            => _readService.SearchEventsAsync(req);

        public Task<Event> GetByIdAsync(int id)
            => _readService.GetByIdAsync(id);

        public Task<Event> CreateAsync(CreateEventDto dto)
            => _writeService.CreateEventAsync(dto);
    }
}
