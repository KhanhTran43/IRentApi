 using Domain.Model.Entity;
using iRentApi.Model.Entity.Contract;
using Microsoft.AspNetCore.Components;
using System.ComponentModel.DataAnnotations.Schema;

namespace iRentApi.Model.Entity
{
    public class RentedWarehouseInfo : EntityBase
    {
        public long? RenterId { get; set; }
        public User? Renter { get; set; }
        public long WarehouseId { get; set; }
        public Warehouse Warehouse { get; set; }
        public DateTime RentedDate { get; set; }
        public DateTime? ConfirmDate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal Total { get; set; }
        public decimal Confirm { get; set; }
        public decimal Deposit { get; set; }
        public string DepositPayment { get; set; }
        public RentedWarehouseStatus Status { get; set; }
        public string ContractBase64 { get; set; }
        public string Hash { get; set; }
        public ICollection<RentingExtend>? Extends { get; set; }
    }
}
