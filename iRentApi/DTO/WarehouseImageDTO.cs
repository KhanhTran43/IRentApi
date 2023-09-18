using iRentApi.DTO.Contract;
using iRentApi.Model.Entity;

namespace iRentApi.DTO
{
    public class WarehouseImageDTO : ISelectDTO<WarehouseImage>, IUpdateDTO<WarehouseImage>, IInsertDTO<WarehouseImage>
    {
        public long Id { get; set; }
        public string Image { get; set; }
    }
}
