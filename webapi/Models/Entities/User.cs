using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace webapi.Models.Entities
{
    [Index(nameof(Email), IsUnique = true)]
    public class User
    {
        [Key]
        [Required]
        public Guid Id { get; set; }
        
        [Required]
        public string Username { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public bool IsSuperUser { get; set; }
    }
}
