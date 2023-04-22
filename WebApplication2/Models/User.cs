using System.ComponentModel.DataAnnotations;

namespace AnimeWebAPI.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(15)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(15)]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        public DateTime? LastLogin { get; set; }

        public int FailedLoginAttempts { get; set; }

        public bool IsBlocked { get; set; }
    }
}
