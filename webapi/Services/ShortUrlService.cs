using System.Text.RegularExpressions;
using webapi.Data;

namespace webapi.Services
{
    public class ShortUrlService
    {
        private readonly IShortUrlRepo _repository;

        public ShortUrlService(IShortUrlRepo repository)
        {
            _repository = repository;
        }

        virtual public async Task<List<string>> ValidateShortenedUrl(string shortenedUrl)
        {
            var errors = new List<string>();

            if (await _repository.IsShortenedUrlExist(shortenedUrl))
            {
                errors.Add("This short url already exist");
            }

            if (!Regex.Match(shortenedUrl, @"^[a-zA-Z0-9]*$").Success)
            {
                errors.Add("Shortened Url must consist only of: lowercase and uppercase letters; numbers.");
            }
            return errors;
        }
    }
}
