using AutoMapper;
using iRentApi.DTO;
using iRentApi.MapperConfigs.TypeConverters;
using iRentApi.Model.Entity;
using System.Text.Json;

namespace iRentApi.MapperConfigs.Profiles
{
    public class WarehouseImageProfiles : Profile
    {
        public WarehouseImageProfiles() {
            CreateMap<ClientWarehouseImage, WarehouseImage>().ForMember(dest => dest.Image, opt =>
            {
                opt.MapFrom<ClientWarehouseImageToWarehouseImage>();
            });
            CreateMap<WarehouseImage, ClientWarehouseImage>().ConvertUsing((src, dest) =>
            {
                return JsonSerializer.Deserialize<ClientWarehouseImage>(src.Image);
            });
        }
    }
}
