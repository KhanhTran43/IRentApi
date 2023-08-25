using iRentApi.DTO.Contract;
using iRentApi.Model.Entity;

namespace iRentApi.DTO
{
    public class RentedWarehouseDTO : ISelectDTO<RentedWarehouse>, IInsertDTO<RentedWarehouse>, IUpdateDTO<RentedWarehouse>
    {
        public long Id { get; set; }
        public long RenterId { get; set; }
        public long WarehouseId { get; set; }
        public DateTime RentedDate { get; set; }
        public DateTime EndDate { get; set; }
        public string ContractBase64 { get; set; }
    }
}
