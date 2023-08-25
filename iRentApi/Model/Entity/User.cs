using iRentApi.Model.Entity;
using iRentApi.Model.Entity.Contract;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Domain.Model.Entity
{
    [Index(nameof(Email), IsUnique = true)]
    public class User : EntityBase
    {
        public string Name { get; set; }
        public string Password { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string Address { get; set; }
        public string Ioc { get; set; }
        public DateTime Dob { get; set; }

        [Phone]
        public string PhoneNumber { get; set; }

        public string? RefreshToken { get; set; }

        public Role Role { get; set; }

        public ICollection<Warehouse> Warehouses { get; set; }
    }
}
