using iRentApi.Model.Entity.Contract;
using System.ComponentModel.DataAnnotations.Schema;

namespace iRentApi.Model.Entity
{
    [Table("Contract")]
    public class ContractModel : EntityBase 
    {
        public long RentedWarehouseId { get; set; }
        public RentedWarehouse RentedWarehouse { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string URL { get; set; }
        public bool Actived { get; set; }
    }
}
