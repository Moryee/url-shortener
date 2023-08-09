using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace webapi.Models.Entities
{
    [Index(nameof(ShortenedUrl), IsUnique = true)]
    public class ShortUrl
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
