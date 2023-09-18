using iRentApi.DTO.Contract;
using iRentApi.Model.Entity;

namespace iRentApi.DTO
{
    public class CreateWarehouseDTO : IInsertDTO<Warehouse>
    {
        public long UserId { get; set; }
        public string Name { get; set; }
        public Ward Ward { get; set; }
        public string Address { get; set; }
        public decimal Price { get; set; }
        public decimal Area { get; set; }
        public int Doors { get; set; }
        public int Floors { get; set; }
        public DateTime CreatedDate { get; set; }
        public List<ClientWarehouseImage> Images { get; set; } = new List<ClientWarehouseImage>();
    }
}
