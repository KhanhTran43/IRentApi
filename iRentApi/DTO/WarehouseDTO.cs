using Domain.Model.Entity;
using iRentApi.DTO.Contract;
using iRentApi.Model.Entity;

namespace iRentApi.DTO
{
    public class WarehouseDTO : ISelectDTO<Warehouse>, IInsertDTO<Warehouse>, IUpdateDTO<Warehouse>
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public decimal Price { get; set; }
        public bool Rented { get; set; }
        public decimal Area { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
