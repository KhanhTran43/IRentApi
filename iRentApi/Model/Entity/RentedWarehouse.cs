using iRentApi.Model.Entity.Contract;

namespace iRentApi.Model.Entity
{
    public class RentedWarehouse : EntityBase
    {
        public long OwnerId { get; set; }
        public long RentedId { get; set; }
        public long WareHouseId { get; set; }
        public DateTime Date { get; set; }
    }
}
