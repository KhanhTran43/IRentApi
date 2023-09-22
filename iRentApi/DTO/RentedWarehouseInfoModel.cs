using iRentApi.Model.Entity;

namespace iRentApi.DTO
{
    public class RentedWarehouseInfoModel
    {
        public long Id { get; set; }
        public long? RenterId { get; set; }
        public DateTime RentedDate { get; set; }
        public DateTime? ConfirmDate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal Total { get; set; }
        public decimal Confirm { get; set; }
        public decimal Deposit { get; set; }
        public RentedWarehouseStatus Status { get; set; }
        public string ContractBase64 { get; set; }
    }
}
