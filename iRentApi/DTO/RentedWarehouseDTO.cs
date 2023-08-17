using iRentApi.DTO.Contract;
using iRentApi.Model.Entity;

namespace iRentApi.DTO
{
    public class RentedWarehouseDTO : ISelectDTO<RentedWarehouse>, IInsertDTO<RentedWarehouse>, IUpdateDTO<RentedWarehouse>
    {
        public long Id { get; set; }
        public long OwnerId { get; set; }
        public long RentedId { get; set; }
        public long WareHouseId { get; set; }
        public DateTime Date { get; set; }
    }
}
