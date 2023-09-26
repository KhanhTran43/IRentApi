using AutoMapper;
using iRentApi.DTO;
using iRentApi.MapperConfigs.ValueResolvers;
using iRentApi.Model.Entity;

namespace iRentApi.MapperConfigs.Profiles
{
    public class RentedWarehouseProfile : Profile
    {
        public RentedWarehouseProfile()
        {
            CreateMap<RentedWarehouseInfo, RentedWarehouseDTO>().ConvertUsing<RentedWarehouseToRentedWarehouseDTOTypeConverter>();
            CreateMap<Warehouse, RentedWarehouseDTO>();
            CreateMap<RentedWarehouseInfo, RentedWarehouseInfoModel>();
            CreateMap<CreateRentedWarehouseDTO, RentedWarehouseInfo>();
            CreateMap<CreateExtendRentingDTO, RentingExtend>();
            CreateMap<RentingExtend, RentingExtendDTO>();
        }
    }
}
