using Domain.Model.Entity;
using iRentApi.DTO;
using iRentApi.Model.Entity.Contract;

namespace iRentApi.Model.Entity
{
    public class Warehouse : EntityBase
    {
        public long? UserId { get; set; }
        public User? User { get; set; }
        public string Name { get; set; }
        public Ward Ward { get; set; }
        public string Address { get; set; }
        public decimal Price { get; set; }
        public decimal Area { get; set; }
        public int Doors { get; set; }
        public int Floors { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Description { get; set; }
        public string? RejectedReason { get; set; }
        public WarehouseStatus Status { get; set; } = WarehouseStatus.Pending;
        public ICollection<RentedWarehouseInfo> RentedWarehouses { get; set; }
        public ICollection<WarehouseComment> Comments { get; set; }
        public ICollection<WarehouseImage> Images { get; set; }
    }

    public enum WarehouseStatus
    {
        Pending,
        Accepted,
        Rejected,
    }
}
