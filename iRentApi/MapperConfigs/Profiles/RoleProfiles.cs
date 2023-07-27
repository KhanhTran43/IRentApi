using AutoMapper;
using Domain.Model.Entity;
using iRentApi.DTO;

namespace iRentApi.MapperConfigs.Profiles
{
    public class RoleProfiles : Profile
    {
        public RoleProfiles() {
            CreateMap<Role, RoleDTO>().ReverseMap();
        }
    }
}
