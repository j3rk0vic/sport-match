using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sport_Match.Models;
using Sport_Match.Services;
using Sport_Match.Services.Notification;

public class AppointmentsController : Controller
{
    private readonly IAppointmentService _appointmentService;
    private readonly IReminderService _reminderService;

    public AppointmentsController(
        IAppointmentService appointmentService,
        IReminderService reminderService)
    {
        _appointmentService = appointmentService;
        _reminderService = reminderService;
    }

    [HttpGet]
    public IActionResult SelectType()
    {
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var appointments = await _appointmentService.GetAllAsync();
        return View(appointments);
    }

    public IActionResult Create(string? type)
    {
        ViewBag.Type = type;
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Appointment appointment)
    {
        if (!ModelState.IsValid)
        {
            return View(appointment);
        }

        await _appointmentService.CreateAsync(appointment);

        await _reminderService.ScheduleAsync(appointment);

        return RedirectToAction("Created");
    }

    public IActionResult Created()
    {
        return View();
    }

    [Authorize]
    public async Task<IActionResult> SyncToGoogle()
    {
        var accessToken = await HttpContext.GetTokenAsync("access_token");

        if (accessToken == null)
            return Content("Greška: Nema Google tokena.");

        await _appointmentService.SyncToGoogleAsync(accessToken);

        return Content("Google kalendar je sinkroniziran!");
    }
}
