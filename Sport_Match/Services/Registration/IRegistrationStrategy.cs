using Sport_Match.Models;

namespace Sport_Match.Services.Registration
{
    public interface IRegistrationStrategy
    {
        Task<string> RegisterAsync(Event ev, int userId, Func<EventRegistration, Task> addRegistration, Func<Task> save);
    }
}
