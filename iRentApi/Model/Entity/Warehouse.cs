using Domain.Model.Entity;
using iRentApi.Model.Entity.Contract;

namespace iRentApi.Model.Entity
{
    public class Warehouse : EntityBase
    {
        public long UserId { get; set; }
        public User User { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public decimal Price { get; set; }
        public bool Rented { get; set; } = false;
        public decimal Area { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
