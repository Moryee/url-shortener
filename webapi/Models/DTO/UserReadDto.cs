using System.ComponentModel.DataAnnotations;

namespace webapi.Models.DTO
{
    public class UserReadDto
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
