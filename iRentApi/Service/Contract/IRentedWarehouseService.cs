using AutoMapper;
using Data.Context;
using iRentApi.Helpers;
using iRentApi.Model.Entity;
using Microsoft.Extensions.Options;

namespace iRentApi.Service.Contract
{
    public abstract class IRentedWarehouseService : IRentCRUDService<RentedWarehouse>
    {
        protected IRentedWarehouseService(IRentContext context, IMapper mapper, IOptions<AppSettings> appSettings) : base(context, mapper, appSettings)
        {
        }

        public abstract Task<bool> CheckWarehouseRented(long warehouseId);
    }
}
