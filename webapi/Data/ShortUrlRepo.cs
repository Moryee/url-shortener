using Microsoft.EntityFrameworkCore;
using webapi.Models.Entities;

namespace webapi.Data
{
    public class ShortUrlRepo : IShortUrlRepo
    {
        private readonly AppDbContext _context;

        public ShortUrlRepo(AppDbContext context)
        {
            _context = context;
        }

        public async Task CreateShortUrl(ShortUrl shortUrl)
        {
            if (shortUrl == null)
            {
                throw new ArgumentNullException(nameof(shortUrl));
            }

            await _context.ShortUrls.AddAsync(shortUrl);
        }

        public async Task DeleteShortUrlById(Guid id)
        {
            var existingShortUrl = await GetShortUrlById(id);
            if (existingShortUrl == null)
            {
                throw new ArgumentNullException(nameof(id));
            }
            _context.ShortUrls.Remove(existingShortUrl);
        }

        public async Task<IEnumerable<ShortUrl>> GetAllShortUrls()
        {
            return await _context.ShortUrls.ToListAsync();
        }

        public async Task<ShortUrl?> GetShortUrlById(Guid id)
        {
            return await _context.ShortUrls.FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<ShortUrl?> GetShortUrlByShortUrl(string shortUrl)
        {
            return await _context.ShortUrls.Where(x => x.ShortenedUrl == shortUrl).FirstOrDefaultAsync();
        }

        public async Task<bool> IsShortenedUrlExist(ShortUrl shortUrl)
        {
            var existingShortUrl = await _context.ShortUrls.Where(x => x.ShortenedUrl == shortUrl.ShortenedUrl).FirstOrDefaultAsync();

            if (existingShortUrl == null)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> SaveChanges()
        {
            return await _context.SaveChangesAsync() >= 0;
        }
    }
}
