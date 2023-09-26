
using iRentApi.DTO.Contract;

namespace iRentApi.DTO
{
    public class RentingExtendDTO
    {
        public long RentedWarehouseInfoId { get; set; }
        public int Duration { get; set; }
        public DateTime ExtendDate { get; set; }
        public decimal Total { get; set; }
    }
}
