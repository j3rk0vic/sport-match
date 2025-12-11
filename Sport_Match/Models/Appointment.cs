namespace Sport_Match.Models
{
    public class Appointment
    {
        public int Id { get; set; }

        public string? Title { get; set; }
        public DateTime Date { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public string? Location { get; set; }
        public string? Notes { get; set; }

        public string? CreatedByUserId { get; set; }
    }

}
