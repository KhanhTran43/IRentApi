using AutoMapper;
using Domain.Model.Entity;
using iRentApi.DTO;
using iRentApi.MapperConfigs.TypeConverters;
using iRentApi.Model.Entity;

namespace iRentApi.MapperConfigs.Profiles
{
    public class WarehouseProfile : Profile
    {
        public WarehouseProfile()
        {
            CreateMap<Warehouse, WarehouseDTO>().ReverseMap();
            CreateMap<CreateWarehouseDTO, Warehouse>();
        }
    }
}
