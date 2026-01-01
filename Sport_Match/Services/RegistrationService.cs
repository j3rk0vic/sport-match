using Microsoft.EntityFrameworkCore;
using Sport_Match.Data;
using Sport_Match.Models;
using Sport_Match.Repositories;
using Sport_Match.Services;
using Sport_Match.Services.Registration;
using System;

public class RegistrationService : IRegistrationService
{
    private readonly IRegistrationRepository _repo;
    private readonly IRegistrationStrategy _strategy;

    public RegistrationService(IRegistrationRepository repo, IRegistrationStrategy strategy)
    {
        _repo = repo;
        _strategy = strategy;
    }

    public async Task<string> RegisterAsync(int eventId, int userId)
    {
        var ev = await _repo.GetEventByIdAsync(eventId);
        if (ev == null)
            return "Event ne postoji.";

        var already = await _repo.GetRegistrationAsync(eventId, userId);
        if (already != null)
            return already.IsConfirmed ? "vec ste prijavljeni." : "vec ste na listi cekanja.";

        if (ev.CurrentParticipants < ev.Capacity)
        {
            ev.CurrentParticipants++;

            await _repo.AddRegistrationAsync(new EventRegistration
            {
                EventId = eventId,
                UserId = userId,
                IsConfirmed = true
            });

            await _repo.SaveChangesAsync();
            return "uspjesno ste prijavljeni.";
        }

        await _repo.AddRegistrationAsync(new EventRegistration
        {
            EventId = eventId,
            UserId = userId,
            IsConfirmed = false
        });

        await _repo.SaveChangesAsync();
        return "Event je popunjen — dodani ste na listu cekanja.";
    }

    public Task RegisterAsync(int eventId, object userId)
    {
        throw new NotImplementedException();
    }

    public async Task<string> UnregisterAsync(int eventId, int userId)
    {
        var registration = await _repo.GetRegistrationAsync(eventId, userId);
        if (registration == null)
            return "Niste bili prijavljeni.";

        var ev = await _repo.GetEventByIdAsync(eventId);
        if (ev == null)
            return "Event ne postoji.";

        if (registration.IsConfirmed)
        {
            ev.CurrentParticipants--;

            var next = await _repo.GetNextWaitlistedAsync(eventId);
            if (next != null)
            {
                next.IsConfirmed = true;
                ev.CurrentParticipants++;
            }
        }

        _repo.RemoveRegistration(registration);
        await _repo.SaveChangesAsync();

        return "uspjesno ste odjavljeni.";
    }
}
