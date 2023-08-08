using AutoMapper;
using webapi.Models.DTO;
using webapi.Models.Entities;

namespace webapi.Models.Profiles
{
    public class ShortUrlsProfile : Profile
    {
        public ShortUrlsProfile()
        {
            // Source -> Dest
            CreateMap<ShortUrl, ShortUrlReadDto>();
            CreateMap<ShortUrlCreateDto, ShortUrl>();
            CreateMap<ShortUrlUpdateDto, ShortUrl>();
        }
    }
}
