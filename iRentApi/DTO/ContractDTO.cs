using iRentApi.DTO.Contract;
using iRentApi.Model.Entity;

namespace iRentApi.DTO
{
    public class ContractDTO : ISelectDTO<ContractModel>, IInsertDTO<ContractModel>, IUpdateDTO<ContractModel>
    {
        public long Id { get; set; }
        public long RentedWarehouseId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string URL { get; set; }
        public bool Actived { get; set; }
    }
}
