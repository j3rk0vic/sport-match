namespace Sport_Match.Models
{
    public class PenaltyRule
    {
        public int Id { get; set; }

       
        public int LateCancellationPenalty { get; set; }

        
        public bool NoShowEnabled { get; set; }
    }
}
