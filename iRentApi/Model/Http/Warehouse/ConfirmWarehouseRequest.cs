using iRentApi.Model.Entity;

namespace iRentApi.Model.Http.Warehouse
{
    public class ConfirmWarehouseRequest
    {
        public WarehouseStatus Status { get; set; }
        public string? RejectedReason { get; set; }
    }
}
