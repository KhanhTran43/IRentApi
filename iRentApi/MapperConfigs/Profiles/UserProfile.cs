using AutoMapper;
using Domain.Model.Entity;
using iRentApi.DTO;
using Microsoft.Win32;
using NuGet.Protocol.Plugins;

namespace iRentApi.MapperConfigs.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDTO>().ReverseMap();
        }
    }
}
