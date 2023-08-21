using AutoMapper;
using Data.Context;
using iRentApi.DTO;
using iRentApi.Helpers;
using iRentApi.Model.Entity;
using iRentApi.Service.Contract;
using Microsoft.Extensions.Options;
using System.Linq;

namespace iRentApi.Service.Implement
{
    public class WarehouseService : IWarehouseService
    {
        public WarehouseService(IRentContext context, IMapper mapper, IOptions<AppSettings> appSettings) : base(context, mapper, appSettings)
        {
            DefaultSelectOptions = new SelectOptions()
            {
                Includes = new string[] { "RentedWarehouses" }
            };
        }

        public override Task<List<WarehouseDTO>> GetOwnerWarehouseList(long userId, SelectOptions? options = null)
        {
            return SelectAll<WarehouseDTO>(options, warehoue => warehoue.UserId == userId);
        }

        public override async Task<List<WarehouseDTO>> GetRenterWarehouseList(long userId, SelectOptions? options = null)
        {
            var warehouseDto = await SelectAll<WarehouseDTO>(options);
            
            return warehouseDto.Where(w => w.RentedInfo?.RenterId == userId).ToList();
        }
    }
}
