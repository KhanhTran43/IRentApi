using iRentApi.Model.Entity;

namespace iRentApi.Service.Contract
{
    public interface IRentedWarehouseService : IEntityCRUDService<RentedWarehouse>
    {
        Task<bool> CheckWarehouseRented(long warehouseId);
    }
}
