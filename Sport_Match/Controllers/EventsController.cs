using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Sport_Match.Dtos;
using Sport_Match.Services;
using Sport_Match.Models;

namespace SportMatch.Controllers
{
    public class EventsController : Controller
    {
        private readonly IEventService _eventService;
        private readonly IRegistrationService _registrationService;

        public EventsController(IEventService eventService, IRegistrationService registrationService)
        {
            _eventService = eventService;
            _registrationService = registrationService;
        }

       
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateEventDto dto)
        {
            if (!ModelState.IsValid)
            {
                return View(dto);
            }

            await _eventService.CreateEventAsync(dto);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Index([FromQuery] EventSearchRequest req)
        {
            var events = await _eventService.SearchEventsAsync(req);
            return View(events);
        }

    
        [HttpPost]
        public async Task<IActionResult> Register(int eventId)
        {
            int userId = 1; // TEMP
            var message = await _registrationService.RegisterAsync(eventId, userId);

            TempData["Status"] = message;
            return RedirectToAction("Details", new { id = eventId });
        }

        [HttpPost]
        public async Task<IActionResult> Unregister(int eventId)
        {
            int userId = 1;
            var message = await _registrationService.UnregisterAsync(eventId, userId);

            TempData["Status"] = message;
            return RedirectToAction("Details", new { id = eventId });
        }

        public async Task<IActionResult> Details(int id)
        {
            var ev = await _eventService.GetByIdAsync(id);
            if (ev == null) return NotFound();

            return View(ev);
        }

      

    }
}
