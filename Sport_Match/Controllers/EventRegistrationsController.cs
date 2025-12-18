using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Sport_Match.Services;

namespace SportMatch.Controllers
{
    public class EventRegistrationsController : Controller
    {
        private readonly IRegistrationService _registrationService;

        public EventRegistrationsController(IRegistrationService registrationService)
        {
            _registrationService = registrationService;
        }

        [HttpPost]
        public async Task<IActionResult> Register(int eventId)
        {
            int userId = 1;
            var message = await _registrationService.RegisterAsync(eventId, userId);

            TempData["Status"] = message;
            return RedirectToAction("Details", "Events", new { id = eventId });
        }

        [HttpPost]
        public async Task<IActionResult> Unregister(int eventId)
        {
            int userId = 1;
            var message = await _registrationService.UnregisterAsync(eventId, userId);

            TempData["Status"] = message;
            return RedirectToAction("Details", "Events", new { id = eventId });
        }
    }
}

