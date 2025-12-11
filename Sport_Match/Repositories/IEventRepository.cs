
using Sport_Match.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sport_Match.Repositories
{
    public interface IEventRepository
    {
        Task AddAsync(Event ev);
        Task<Event> GetByIdAsync(int id);
        Task<List<Event>> GetAllAsync();
        Task SaveChangesAsync();
    }
}
