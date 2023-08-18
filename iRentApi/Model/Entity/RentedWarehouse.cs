using Domain.Model.Entity;
using iRentApi.Model.Entity.Contract;
using Microsoft.AspNetCore.Components;
using System.ComponentModel.DataAnnotations.Schema;

namespace iRentApi.Model.Entity
{
    public class RentedWarehouse : EntityBase
    {
        public long? RenterId { get; set; }
        [ForeignKey("RenterId")]
        public User? Renter { get; set; }
        public long WareHouseId { get; set; }
        [ForeignKey("WareHouseId")]
        public Warehouse WareHouse { get; set; }
        public DateTime RentedDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
