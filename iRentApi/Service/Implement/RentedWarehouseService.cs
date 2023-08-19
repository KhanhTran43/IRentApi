using AutoMapper;
using Data.Context;
using iRentApi.Helpers;
using iRentApi.Service.Contract;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace iRentApi.Service.Implement
{
    public class RentedWarehouseService : IRentedWarehouseService
    {
        public RentedWarehouseService(IRentContext context, IMapper mapper, IOptions<AppSettings> appSettings) : base(context, mapper, appSettings)
        {
        }

        public override Task<bool> CheckWarehouseRented(long warehouseId)
        {
            DateTime now = DateTime.Now;
            return this.Context.RentedWarehouses.Where(rw => warehouseId == rw.WarehouseId && rw.EndDate.CompareTo(now) >= 0).AnyAsync();
        }
    }
}
