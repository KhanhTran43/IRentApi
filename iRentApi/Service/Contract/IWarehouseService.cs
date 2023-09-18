using AutoMapper;
using Data.Context;
using iRentApi.DTO;
using iRentApi.Helpers;
using iRentApi.Model.Entity;
using iRentApi.Model.Service.Crud;
using iRentApi.Service.Implement;
using Microsoft.Extensions.Options;

namespace iRentApi.Service.Contract
{
    public abstract class IWarehouseService : IRentCRUDService<Warehouse>
    {
        protected IWarehouseService(IRentContext context, IMapper mapper, IOptions<AppSettings> appSettings) : base(context, mapper, appSettings)
        {
        }

        public abstract Task<List<WarehouseDTO>> GetOwnerWarehouseList(long userId, GetStaticRequest? options = null);
        public abstract Task<List<WarehouseDTO>> GetRenterWarehouseList(long userId, GetStaticRequest? options = null);
        public abstract Task<TMap> AddComment<TMap>(long warehouseId, long userId, CreateWarehouseCommentDTO warehouse);
    }
}
