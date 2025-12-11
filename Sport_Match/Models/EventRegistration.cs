namespace Sport_Match.Models
{
    
        public class EventRegistration
        {
            public int Id { get; set; }

            public int EventId { get; set; }
            public Event Event { get; set; }

            public int UserId { get; set; }  
            public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

            public bool IsConfirmed { get; set; }  
        }

    }

