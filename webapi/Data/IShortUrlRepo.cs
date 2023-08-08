using webapi.Models.Entities;

namespace webapi.Data
{
    public interface IShortUrlRepo
    {
        Task<bool> SaveChanges();

        Task<IEnumerable<ShortUrl>> GetAllShortUrls();
        Task<ShortUrl?> GetShortUrlById(Guid id);
        Task CreateShortUrl(ShortUrl shortUrl);
        Task DeleteShortUrlById(Guid id);

        Task<bool> IsShortenedUrlExist(ShortUrl shortUrl);
        Task<ShortUrl?> GetShortUrlByShortUrl(string shortUrl);
    }
}
