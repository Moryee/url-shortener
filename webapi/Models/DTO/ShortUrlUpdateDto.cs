using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace webapi.Models.DTO
{
    public class ShortUrlUpdateDto
    {
        public string Url { get; set; }

        public string ShortenedUrl { get; set; }
    }
}
