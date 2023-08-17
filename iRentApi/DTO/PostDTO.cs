using iRentApi.DTO.Contract;
using iRentApi.Model.Entity;

namespace iRentApi.DTO
{
    public class PostDTO : ISelectDTO<Post>, IInsertDTO<Post>, IUpdateDTO<Post>
    {
        public long Id { get; set; }
        public long WareHouseId { get; set; }
        public string Header { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public float Rate { get; set; }
    }
}
