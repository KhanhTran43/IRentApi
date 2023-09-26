using iRentApi.Model.Entity.Contract;

namespace iRentApi.Model.Entity
{
    public class RentingExtend : EntityBase
    {
        public long RentedWarehouseInfoId { get; set; }
        public RentedWarehouseInfo RentedWarehouseInfo { get; set; }
        public int Duration { get; set; }
        public DateTime ExtendDate { get; set; }
        public decimal Total { get; set; }
        public string Hash { get; set; }
    }
}
