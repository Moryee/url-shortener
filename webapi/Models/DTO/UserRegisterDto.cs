using System.ComponentModel.DataAnnotations;

namespace webapi.Models.DTO
{
    public class UserRegisterDto
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

    }
}
