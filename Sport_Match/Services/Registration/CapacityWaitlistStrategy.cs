using Sport_Match.Models;

namespace Sport_Match.Services.Registration
{
    public class CapacityWaitlistStrategy : IRegistrationStrategy
    {
        public async Task<string> RegisterAsync(
            Event ev,
            int userId,
            Func<EventRegistration, Task> addRegistration,
            Func<Task> save)
        {
            if (ev.CurrentParticipants < ev.Capacity)
            {
                ev.CurrentParticipants++;

                await addRegistration(new EventRegistration
                {
                    EventId = ev.Id,
                    UserId = userId,
                    IsConfirmed = true
                });

                await save();
                return "Uspješno ste prijavljeni.";
            }

            await addRegistration(new EventRegistration
            {
                EventId = ev.Id,
                UserId = userId,
                IsConfirmed = false
            });

            await save();
            return "Event je popunjen — dodani ste na listu čekanja.";
        }
    }
}
