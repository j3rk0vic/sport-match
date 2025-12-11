using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sport_Match.Models;
using Sport_Match.Services;

public class AppointmentsController : Controller
{
    private readonly GoogleCalendarService _calendarService;

    public AppointmentsController(GoogleCalendarService calendarService)
    {
        _calendarService = calendarService;
    }

    // PRVA STRANICA – izbor trening/utakmica
    public IActionResult Index()
    {
        return View("SelectType");
    }

    // CREATE GET
    public IActionResult Create(string? type)
    {
        ViewBag.Type = type;
        return View();
    }

    // CREATE POST
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Appointment appointment)
    {
        if (!ModelState.IsValid)
        {
            return View(appointment);
        }

        // Nakon spremanja u Google Calendar
        return RedirectToAction("Created");
    }

    public IActionResult Created()
    {
        return View();
    }

    [Authorize]
    public async Task<IActionResult> AddToGoogle()
    {
        var accessToken = await HttpContext.GetTokenAsync("access_token");

        // ovdje bi trebala biti logika dodavanja eventa u Google Calendar
        return Redirect("https://calendar.google.com");
    }


    [Authorize]
    public async Task<IActionResult> SyncToGoogle()
    {
        var accessToken = await HttpContext.GetTokenAsync("access_token");

        if (accessToken == null)
            return Content("Greška: Nema Google tokena.");

        // Ovdje kreiraš event ponovo ili čuvaš u Session prije
        // Za sada samo poruka:
        return Content("Google kalendar je sinkroniziran!");
    }

}
