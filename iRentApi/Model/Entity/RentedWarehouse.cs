﻿using Domain.Model.Entity;
using iRentApi.Model.Entity.Contract;
using Microsoft.AspNetCore.Components;
using System.ComponentModel.DataAnnotations.Schema;

namespace iRentApi.Model.Entity
{
    public class RentedWarehouse : EntityBase
    {
        public long? RenterId { get; set; }
        public User? Renter { get; set; }
        public long WarehouseId { get; set; }
        public Warehouse Warehouse { get; set; }
        public DateTime RentedDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
