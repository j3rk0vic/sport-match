using System;
using System.ComponentModel.DataAnnotations;

namespace Sport_Match.Dtos
{
    public class CreateEventDto
    {
        [Required]
        [Display(Name = "Naziv događaja")]
        public string Name { get; set; }

        [Required]
        public string Sport { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [Required]
        [DataType(DataType.Time)]
        public TimeSpan Time { get; set; }

        [Required]
        public string Location { get; set; }

        [Display(Name = "Privatno")]
        public bool IsPrivate { get; set; }
    }
}
