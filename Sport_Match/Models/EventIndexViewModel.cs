using Sport_Match.Dtos;

namespace Sport_Match.Models
{
    public class EventIndexViewModel
    {
        public EventSearchRequest Search { get; set; } = new();
        public List<Event> Events { get; set; } = new();

        public List<string> Sports { get; set; } = new();
    }
}
