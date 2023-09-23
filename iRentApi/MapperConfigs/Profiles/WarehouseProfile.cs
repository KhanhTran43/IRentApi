using AutoMapper;
using Domain.Model.Entity;
using iRentApi.DTO;
using iRentApi.Helpers;
using iRentApi.MapperConfigs.TypeConverters;
using iRentApi.Model.Entity;

namespace iRentApi.MapperConfigs.Profiles
{
    public class WarehouseProfile : Profile
    {
        public WarehouseProfile()
        {
            CreateMap<Warehouse, WarehouseDTO>()
                .ForMember(dest => dest.RentedInfo, opt =>
                {
                    opt.MapFrom((src, dest) =>
                    {
                        var rentedWarehouses = src.RentedWarehouses;
                        if (rentedWarehouses != null && rentedWarehouses.Count > 0)
                        {
                            return rentedWarehouses.Where(rw =>
                                RentedWarehouseUtility.IsRentingStatus(rw.Status))
                            .FirstOrDefault();
                        }
                        else return null;
                    });
                })
                .ForMember(dest => dest.Rented, opt =>
                {
                    opt.MapFrom((src, dest) =>
                    {
                        var rentedWarehouses = src.RentedWarehouses;
                        if (rentedWarehouses != null && rentedWarehouses.Count > 0)
                        {
                            return rentedWarehouses
                            .Where(rw =>
                                RentedWarehouseUtility.IsRentingStatus(rw.Status)
                            )
                            .Any();
                        }
                        else return false;
                    });
                })
                .ReverseMap();
            CreateMap<CreateWarehouseDTO, Warehouse>();
        }
    }
}
