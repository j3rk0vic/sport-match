using Microsoft.EntityFrameworkCore;
using Sport_Match.Data;
using Sport_Match.Models;
using Sport_Match.Services;
using System;

public class RegistrationService : IRegistrationService
{
    private readonly ApplicationDbContext _db;

    public RegistrationService(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<string> RegisterAsync(int eventId, int userId)
    {
        var ev = await _db.Events.FindAsync(eventId);
        if (ev == null)
            return "Event ne postoji.";

        var already = await _db.EventRegistrations
            .FirstOrDefaultAsync(r => r.EventId == eventId && r.UserId == userId);

        if (already != null)
            return already.IsConfirmed ? "Već ste prijavljeni." : "Već ste na listi čekanja.";

       
        if (ev.CurrentParticipants < ev.Capacity)
        {
            ev.CurrentParticipants++;

            _db.EventRegistrations.Add(new EventRegistration
            {
                EventId = eventId,
                UserId = userId,
                IsConfirmed = true
            });

            await _db.SaveChangesAsync();
            return "Uspješno ste prijavljeni.";
        }

        _db.EventRegistrations.Add(new EventRegistration
        {
            EventId = eventId,
            UserId = userId,
            IsConfirmed = false
        });

        await _db.SaveChangesAsync();
        return "Event je popunjen — dodani ste na listu čekanja.";
    }

    public async Task<string> UnregisterAsync(int eventId, int userId)
    {
        var registration = await _db.EventRegistrations
            .FirstOrDefaultAsync(r => r.EventId == eventId && r.UserId == userId);

        if (registration == null)
            return "Niste bili prijavljeni.";

        var ev = await _db.Events.FindAsync(eventId);

        if (registration.IsConfirmed)
        {
            ev.CurrentParticipants--;

            var next = await _db.EventRegistrations
                .Where(r => r.EventId == eventId && !r.IsConfirmed)
                .OrderBy(r => r.CreatedAt)
                .FirstOrDefaultAsync();

            if (next != null)
            {
                next.IsConfirmed = true;
                ev.CurrentParticipants++;
            }
        }

        _db.EventRegistrations.Remove(registration);
        await _db.SaveChangesAsync();

        return "Uspješno ste odjavljeni.";
    }
}
