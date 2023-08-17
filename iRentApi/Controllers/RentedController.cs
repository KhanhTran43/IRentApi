using iRentApi.Controllers.Contract;
using iRentApi.DTO;
using iRentApi.Model.Entity;
using iRentApi.Service.Contract;
using Microsoft.AspNetCore.Mvc;

namespace iRentApi.Controllers
{
    public class RentedWarehouseController : CrudController<RentedWarehouse, RentedWarehouseDTO, RentedWarehouseDTO, RentedWarehouseDTO>
    {
        public RentedWarehouseController(IServiceWrapper serviceWrapper) : base(serviceWrapper)
        {
        }
    }
}
