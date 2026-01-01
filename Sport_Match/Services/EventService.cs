using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Sport_Match.Data;
using Sport_Match.Dtos;
using Sport_Match.Models;
using Sport_Match.Services.Sorting;
using Sport_Match.Services.Factories;

namespace Sport_Match.Services
{
    public class EventService : IEventReadService, IEventWriteService
    {
        private readonly ApplicationDbContext _db;
        private readonly IEnumerable<IEventSortStrategy> _sortStrategies;
        private readonly IEventFactory _eventFactory;

        public EventService(
            ApplicationDbContext db,
            IEnumerable<IEventSortStrategy> sortStrategies,
            IEventFactory eventFactory)
        {
            _db = db;
            _sortStrategies = sortStrategies;
            _eventFactory = eventFactory;
        }

        public IQueryable<Event> Search(EventSearchRequest req)
        {
            var q = _db.Events.AsQueryable();

            if (!string.IsNullOrWhiteSpace(req.Sport))
                q = q.Where(e => e.Sport.Contains(req.Sport));

            if (!string.IsNullOrWhiteSpace(req.Name))
                q = q.Where(e => e.Name.Contains(req.Name));

            if (!string.IsNullOrWhiteSpace(req.Location))
                q = q.Where(e => e.Location.Contains(req.Location));

            if (req.DateFrom != null)
                q = q.Where(e => e.StartDateTime >= req.DateFrom);

            if (req.DateTo != null)
                q = q.Where(e => e.StartDateTime <= req.DateTo);

            if (req.IsPrivate != null)
                q = q.Where(e => e.IsPrivate == req.IsPrivate);

            var strategy = _sortStrategies
                .FirstOrDefault(s => s.CanHandle(req.SortBy));

            if (strategy != null)
            {
                q = strategy.Apply(q, req.SortDesc);
            }
            else
            {
                q = req.SortDesc
                    ? q.OrderByDescending(e => e.StartDateTime)
                    : q.OrderBy(e => e.StartDateTime);
            }

            return q
                .Skip((req.Page - 1) * req.PageSize)
                .Take(req.PageSize);
        }

        public Task<IEnumerable<Event>> SearchEventsAsync(EventSearchRequest req)
        {
            IEnumerable<Event> result = Search(req).ToList();
            return Task.FromResult(result);
        }

        public async Task<Event> CreateEventAsync(CreateEventDto dto)
        {
            // 🔹 OVDJE JE FACTORY METHOD
            var ev = _eventFactory.Create(dto);

            await _db.Events.AddAsync(ev);
            await _db.SaveChangesAsync();
            return ev;
        }

        public async Task<Event> GetByIdAsync(int id)
        {
            return await _db.Events.FindAsync(id);
        }
    }
}
