using iRentApi.Controllers.Contract;
using iRentApi.DTO;
using iRentApi.Model.Entity;
using iRentApi.Service.Database;
using Microsoft.AspNetCore.Mvc;

namespace iRentApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RentedWarehouseController : CrudController<RentedWarehouse, RentedWarehouseDTO, CreateRentedWarehouseDTO, RentedWarehouseDTO>
    {
        public RentedWarehouseController(IUnitOfWork serviceWrapper) : base(serviceWrapper)
        {
        }

        [HttpGet("{warehouseId}/rented")]
        public async Task<ActionResult<bool>> CheckWarehouseRented([FromRoute] long warehouseId)
        {
            return await Service.RentedWarehouseService.CheckWarehouseRented(warehouseId);
        }
    }
}
