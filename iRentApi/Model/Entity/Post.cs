using iRentApi.Model.Entity.Contract;

namespace iRentApi.Model.Entity
{
    public class Post : EntityBase
    {
        public long WarehouseId { get; set; }
        public Warehouse Warehouse { get; set; }
        public string Header { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public float Rate { get; set; }
    }
}
