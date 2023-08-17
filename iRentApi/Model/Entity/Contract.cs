using iRentApi.Model.Entity.Contract;

namespace iRentApi.Model.Entity
{
    public class ContractModel : EntityBase 
    {
        public long RentedWarehouseId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string URL { get; set; }
        public bool Actived { get; set; }
    }
}
