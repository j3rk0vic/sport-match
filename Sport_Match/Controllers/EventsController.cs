using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Sport_Match.Dtos;
using Sport_Match.Services.Facades;

namespace SportMatch.Controllers
{
    public class EventsController : Controller
    {
        private readonly IEventFacade _eventFacade;

        public EventsController(IEventFacade eventFacade)
        {
            _eventFacade = eventFacade;
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

            await _eventFacade.CreateAsync(dto);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Index([FromQuery] EventSearchRequest req)
        {
            var events = await _eventFacade.SearchAsync(req);
            return View(events);
        }

        public async Task<IActionResult> Details(int id)
        {
            var ev = await _eventFacade.GetByIdAsync(id);
            if (ev == null)
                return NotFound();

            return View(ev);
        }
    }
}
