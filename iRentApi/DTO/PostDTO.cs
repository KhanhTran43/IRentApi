using iRentApi.DTO.Contract;
using iRentApi.Model.Entity;

namespace iRentApi.DTO
{
    public class PostDTO : ISelectDTO<Warehouse>, IInsertDTO<Warehouse>, IUpdateDTO<Warehouse>
    {
        public long Id { get; set; }
        public long WareHouseId { get; set; }
        public string Header { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public float Rate { get; set; }
    }
}
