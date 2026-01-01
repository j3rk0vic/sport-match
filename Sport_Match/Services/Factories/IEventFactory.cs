using Sport_Match.Dtos;
using Sport_Match.Models;

namespace Sport_Match.Services.Factories
{
    public interface IEventFactory
    {
        Event Create(CreateEventDto dto);
    }
}
