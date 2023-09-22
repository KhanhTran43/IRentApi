using AutoMapper;
using Data.Context;
using iRentApi.DTO;
using iRentApi.DTO.Contract;
using iRentApi.Helpers;
using iRentApi.Model.Entity;
using iRentApi.Model.Service.Crud;
using iRentApi.Service.Database.Contract;
using iRentApi.Service.Database.Exception;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Options;

namespace iRentApi.Service.Database.Implement
{
    public class RentedWarehouseService : IRentedWarehouseService
    {
        public RentedWarehouseService(IRentContext context, IMapper mapper, IOptions<AppSettings> appSettings) : base(context, mapper, appSettings)
        {
        }

        public override EntityEntry<RentedWarehouseInfo> Insert(IInsertDTO<RentedWarehouseInfo> insert)
        {
            var entityEntry = base.Insert(insert);
            entityEntry.Reference(e => e.Warehouse).Load();
            return entityEntry;
        }

        public override Task<bool> CheckWarehouseRented(long warehouseId)
        {
            DateTime now = DateTime.Now;
            return Context.RentedWarehouseInfos.Where(rw => warehouseId == rw.WarehouseId && rw.EndDate.CompareTo(now) >= 0).AnyAsync();
        }

        public override Task<List<RentedWarehouseDTO>> GetRenterWarehouseList(long userId, GetStaticRequest? options = null)
        {
            return SelectAll<RentedWarehouseDTO>(options, rentedWarehouseInfo => rentedWarehouseInfo.RenterId == userId);
        }

        public override async Task Confirm(long rentedWarehouseId)
        {
            var rentedWarehouse = await Context.RentedWarehouseInfos.FindAsync(rentedWarehouseId) ?? throw new EntityNotFoundException();
            if (rentedWarehouse.Status == RentedWarehouseStatus.Waiting)
            {
                rentedWarehouse.Status = RentedWarehouseStatus.Confirmed;
                Context.SaveChanges();
            }
            else throw new InvalidOperationException("Invalid confirm action");
        }
    }
}
