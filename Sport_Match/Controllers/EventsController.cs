using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Sport_Match.Dtos;
using Sport_Match.Services;

namespace SportMatch.Controllers
{
    public class EventsController : Controller
    {
        private readonly IEventService _eventService;

        public EventsController(IEventService eventService)
        {
            _eventService = eventService;
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


            return RedirectToAction("Index", "Events");
        }


        public IActionResult Index()
        {

            return View();
        }
    }
}

