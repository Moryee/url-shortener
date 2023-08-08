using AutoMapper;
using webapi.Models.DTO;
using webapi.Models.Entities;

namespace webapi.Models.Profiles
{
    public class UsersProfile : Profile
    {
        public UsersProfile()
        {
            // Source -> Dest
            CreateMap<User, UserReadDto>();
            CreateMap<UserLoginDto, User>();
            CreateMap<UserRegisterDto, User>();
        }
    }
}
