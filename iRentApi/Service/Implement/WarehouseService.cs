using AutoMapper;
using Data.Context;
using iRentApi.DTO;
using iRentApi.Helpers;
using iRentApi.Model.Entity;
using iRentApi.Service.Contract;
using Microsoft.Extensions.Options;

namespace iRentApi.Service.Implement
{
    public class WarehouseService : IWarehouseService
    {
        public WarehouseService(IRentContext context, IMapper mapper, IOptions<AppSettings> appSettings) : base(context, mapper, appSettings)
        {
        }

        public override Task<List<WarehouseDTO>> GetOwnWarehouseList(long userId, SelectOptions? options = null)
        {
            return SelectAll<WarehouseDTO>(options, warehoue => warehoue.UserId == userId);
        }
    }
}
