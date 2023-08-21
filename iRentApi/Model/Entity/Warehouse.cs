using Domain.Model.Entity;
using iRentApi.DTO;
using iRentApi.Model.Entity.Contract;

namespace iRentApi.Model.Entity
{
    public class Warehouse : EntityBase
    {
        public long? UserId { get; set; }
        public User? User { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public decimal Price { get; set; }
        public decimal Area { get; set; }
        public DateTime CreatedDate { get; set; }
        public virtual ICollection<RentedWarehouse> RentedWarehouses { get; set; }

        public bool GetRented()
        {
            if (RentedWarehouses != null)
            {
                if (RentedWarehouses.Count > 0)
                {
                    return RentedWarehouses.Where(rw => rw.EndDate.CompareTo(DateTime.Now) >= 0).Any();
                }
                else return false;
            }
            else return false;
        }

        public RentedInfo? GetRentedInfo()
        {
            if (RentedWarehouses != null && RentedWarehouses.Count > 0)
            {
                var activedRentedWarehouse = RentedWarehouses.Where(rw => rw.EndDate.CompareTo(DateTime.Now) >= 0).FirstOrDefault();

                if(activedRentedWarehouse != null)
                    return new RentedInfo() { EndDate = activedRentedWarehouse.EndDate, RentedDate = activedRentedWarehouse.RentedDate};
                else 
                    return null;
            }
            else return null;
        }
    }
}
