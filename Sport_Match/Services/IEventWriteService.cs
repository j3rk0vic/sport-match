using Sport_Match.Dtos;
using Sport_Match.Models;
using System.Threading.Tasks;

namespace Sport_Match.Services
{
    public interface IEventWriteService
    {
        Task<Event> CreateEventAsync(CreateEventDto dto);
    }
}
