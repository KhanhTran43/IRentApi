using AutoMapper;
using iRentApi.DTO;
using iRentApi.Model.Entity;

namespace iRentApi.MapperConfigs.ValueResolvers
{
    public class RentedWarehouseToRentedWarehouseDTOTypeConverter : ITypeConverter<RentedWarehouseInfo, RentedWarehouseDTO>
    {
        public RentedWarehouseDTO Convert(RentedWarehouseInfo source, RentedWarehouseDTO destination, ResolutionContext context)
        {
            destination = context.Mapper.Map<RentedWarehouseDTO>(source.Warehouse);
            destination.RentedInfo = context.Mapper.Map<RentedWarehouseInfoModel>(source);

            return destination;
        }
    }
}
