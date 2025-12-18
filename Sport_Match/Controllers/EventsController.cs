using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Sport_Match.Dtos;
using Sport_Match.Services;

namespace SportMatch.Controllers
{
    public class EventsController : Controller
    {
        private readonly IEventReadService _eventReadService;
        private readonly IEventWriteService _eventWriteService;

        public EventsController(
            IEventReadService eventReadService,
            IEventWriteService eventWriteService)
        {
            _eventReadService = eventReadService;
            _eventWriteService = eventWriteService;
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

            
            await _eventWriteService.CreateEventAsync(dto);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Index([FromQuery] EventSearchRequest req)
        {
            
            var events = await _eventReadService.SearchEventsAsync(req);
            return View(events);
        }

        public async Task<IActionResult> Details(int id)
        {
            
            var ev = await _eventReadService.GetByIdAsync(id);
            if (ev == null) return NotFound();

            return View(ev);
        }
    }
}
