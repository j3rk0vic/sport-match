
using Sport_Match.Data;
using Sport_Match.Dtos;
using Sport_Match.Models;

namespace Sport_Match.Services
{
    public class EventService
    {
        private readonly ApplicationDbContext _db;

        public EventService(ApplicationDbContext db)
        {
            _db = db;
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

          
            q = req.SortBy switch
            {
                "Name" => req.SortDesc ? q.OrderByDescending(e => e.Name) : q.OrderBy(e => e.Name),
                "Sport" => req.SortDesc ? q.OrderByDescending(e => e.Sport) : q.OrderBy(e => e.Sport),
                "Location" => req.SortDesc ? q.OrderByDescending(e => e.Location) : q.OrderBy(e => e.Location),
                "IsPrivate" => req.SortDesc ? q.OrderByDescending(e => e.IsPrivate) : q.OrderBy(e => e.IsPrivate),
                _ => req.SortDesc ? q.OrderByDescending(e => e.StartDateTime) : q.OrderBy(e => e.StartDateTime)
            };

          
            return q.Skip((req.Page - 1) * req.PageSize).Take(req.PageSize);
        }
        public async Task<Event> GetByIdAsync(int id)
        {
            return await _db.Events.FindAsync(id);
        }


    }

}
