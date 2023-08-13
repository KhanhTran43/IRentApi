using Domain.Model;
using iRentApi.Model.Entity;
using iRentApi.Model.Entity.Contract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model.Entity
{
    public class User : EntityBase
    {
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string Address { get; set; }
        public DateTime DateOfBirth { get; set; }

        [Phone]
        public string PhoneNumber { get; set; }

        public string? RefreshToken { get; set; }

        public ICollection<Role> Roles { get; set; }
        public ICollection<Warehouse> Warehouses { get; set; }
    }
}
