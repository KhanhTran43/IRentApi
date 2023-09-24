using AutoMapper;
using Data.Context;
using iRentApi.DTO;
using iRentApi.Helpers;
using iRentApi.Model.Entity;
using iRentApi.Model.Service.Crud;
using iRentApi.Service.Database.Contract;
using iRentApi.Service.Database.Exception;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Linq;

namespace iRentApi.Service.Database.Implement
{
    public class WarehouseService : IWarehouseService
    {
        public WarehouseService(IRentContext context, IMapper mapper, IOptions<AppSettings> appSettings) : base(context, mapper, appSettings)
        {
        }

        public async override Task<TMap> AddComment<TMap>(long warehouseId, long userId, CreateWarehouseCommentDTO warehouse)
        {
            var entityEntry = Context.WarehouseComments.Add(Mapper.Map<CreateWarehouseCommentDTO, WarehouseComment>(warehouse));
            await Context.SaveChangesAsync();

            entityEntry.Reference(entity => entity.User).Load();

            return Mapper.Map<WarehouseComment, TMap>(entityEntry.Entity);
        }

        public override async Task ConfirmWarehouse(long warehouseId, WarehouseStatus status, string? rejectedReson = null)
        {
            var warehouse = await Context.Warehouses.FindAsync(warehouseId) ?? throw new EntityNotFoundException();
            if (status != WarehouseStatus.Pending && warehouse.Status == WarehouseStatus.Pending)
            {
                warehouse.Status = status;

                if(status == WarehouseStatus.Rejected && !string.IsNullOrEmpty(rejectedReson))
                {
                    warehouse.RejectedReason = rejectedReson;
                }

                Context.SaveChanges();
            }
            else
                throw new InvalidOperationException("Invalid confirm warehouse action");
        }

        public override Task<List<WarehouseDTO>> GetOwnerWarehouseList(long userId, GetStaticRequest? options = null)
        {
            return SelectAll<WarehouseDTO>(options, warehoue => warehoue.UserId == userId);
        }
    }
}
