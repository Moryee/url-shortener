using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace webapi.Models.DTO
{
    public class ShortUrlUpdateDto
    {
        [Required]
        public string Url { get; set; }

        [Required]
        public string ShortenedUrl { get; set; }
    }
}
