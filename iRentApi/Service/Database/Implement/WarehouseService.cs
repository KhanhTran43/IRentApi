using AutoMapper;
using Data.Context;
using iRentApi.DTO;
using iRentApi.Helpers;
using iRentApi.Model.Entity;
using iRentApi.Model.Service.Crud;
using iRentApi.Service.Database.Contract;
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

        public override Task<List<WarehouseDTO>> GetOwnerWarehouseList(long userId, GetStaticRequest? options = null)
        {
            return SelectAll<WarehouseDTO>(options, warehoue => warehoue.UserId == userId);
        }

        public override async Task<List<WarehouseDTO>> GetRenterWarehouseList(long userId, GetStaticRequest? options = null)
        {
            var warehouseDto = await SelectAll<WarehouseDTO>(options);

            return warehouseDto.Where(w => w.RentedInfo?.RenterId == userId).ToList();
        }
    }
}
