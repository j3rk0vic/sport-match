using System.ComponentModel.DataAnnotations;

namespace Sport_Match.Models
{
    public class User
    {
        public int id { get; set; } // primarni kljuc

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        [Required]
        public string PasswordSalt { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
