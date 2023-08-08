using System.ComponentModel.DataAnnotations.Schema;

namespace webapi.Models.DTO
{
    public class ShortUrlReadDto
    {
        public Guid Id { get; set; }

        public string Url { get; set; }

        public string ShortenedUrl { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }
    }
}
