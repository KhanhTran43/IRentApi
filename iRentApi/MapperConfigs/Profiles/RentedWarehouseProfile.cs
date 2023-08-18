using AutoMapper;
using iRentApi.DTO;
using iRentApi.Model.Entity;

namespace iRentApi.MapperConfigs.Profiles
{
    public class RentedWarehouseProfile : Profile
    {
        public RentedWarehouseProfile()
        {
            CreateMap<RentedWarehouse, RentedWarehouseDTO>().ReverseMap();
        }
    }
}
