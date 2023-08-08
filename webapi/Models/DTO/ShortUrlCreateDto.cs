using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace webapi.Models.DTO
{
    public class ShortUrlCreateDto
    {
        public string Url { get; set; }

        public string ShortenedUrl { get; set; }

        [Key]
        [ForeignKey("User")]
        public string UserId { get; set; }
    }
}
