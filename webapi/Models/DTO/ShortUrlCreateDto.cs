using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace webapi.Models.DTO
{
    public class ShortUrlCreateDto
    {
        [Required]
        public string Url { get; set; }

        [Required]
        public string ShortenedUrl { get; set; }

        [ForeignKey("User")]
        [Required]
        public string UserId { get; set; }
    }
}
