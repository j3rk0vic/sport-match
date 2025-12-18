using System.Linq;
using Sport_Match.Models;

namespace Sport_Match.Services.Sorting
{
    public interface IEventSortStrategy
    {
        bool CanHandle(string sortBy);
        IQueryable<Event> Apply(IQueryable<Event> query, bool desc);
    }
}
