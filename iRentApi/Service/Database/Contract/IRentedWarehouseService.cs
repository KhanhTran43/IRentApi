using AutoMapper;
using Data.Context;
using iRentApi.DTO;
using iRentApi.Helpers;
using iRentApi.Model.Entity;
using iRentApi.Model.Service.Crud;
using Microsoft.Extensions.Options;

namespace iRentApi.Service.Database.Contract
{
    public abstract class IRentedWarehouseService : IRentCRUDService<RentedWarehouseInfo>
    {
        protected IRentedWarehouseService(IRentContext context, IMapper mapper, IOptions<AppSettings> appSettings) : base(context, mapper, appSettings)
        {
        }

        public abstract Task<bool> CheckWarehouseRented(long warehouseId);
        public abstract Task<List<RentedWarehouseDTO>> GetRenterRentedWarehouseList(long userId, GetStaticRequest? options = null);
        public abstract Task<List<RentedWarehouseDTO>> GetOwnerRentedWarehouseList(long userId, GetStaticRequest? options = null);
        public abstract Task Confirm(long rentedWarehouseId);
        public abstract Task RequestCancel(long rentedWarehouseId);
        public abstract Task ConfirmCancel(long rentedWarehouseId);
    }
}
