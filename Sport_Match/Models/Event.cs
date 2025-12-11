using System;
using System.ComponentModel.DataAnnotations;

namespace Sport_Match.Models
{
    public class Event
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [MaxLength(50)]
        public string Sport { get; set; }

        [Required]
        public DateTime StartDateTime { get; set; }

        [Required]
        [MaxLength(200)]
        public string Location { get; set; }

        public bool IsPrivate { get; set; }

        public int Capacity { get; set; }       
        public int CurrentParticipants { get; set; } 


    }
}
