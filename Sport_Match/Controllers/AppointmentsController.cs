using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sport_Match.Models;
using Sport_Match.Services;

public class AppointmentsController : Controller
{
    private readonly IAppointmentService _appointmentService;

    public AppointmentsController(IAppointmentService appointmentService)
    {
        _appointmentService = appointmentService;
    }

    public IActionResult Index()
    {
        return View("SelectType");
    }

    public IActionResult Create(string? type)
    {
        ViewBag.Type = type;
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Appointment appointment)
    {
        if (!ModelState.IsValid)
        {
            return View(appointment);
        }

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
