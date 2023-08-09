using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace webapi.Models.DTO
{
    public class ShortUrlReadDto
    {
        [Key]
        [Required]
        public Guid Id { get; set; }

        [Required]
        public string Url { get; set; }

        [Required]
        public string ShortenedUrl { get; set; }

        [ForeignKey("User")]
        [Required]
        public string UserId { get; set; }
    }
}
