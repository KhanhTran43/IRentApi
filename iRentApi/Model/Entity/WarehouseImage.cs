using iRentApi.Model.Entity.Contract;

namespace iRentApi.Model.Entity
{
    public class WarehouseImage : EntityBase
    {
        public long WarehouseId;
        public Warehouse Warehouse { get; set; }
        public string Image { get; set; }
    }
}
