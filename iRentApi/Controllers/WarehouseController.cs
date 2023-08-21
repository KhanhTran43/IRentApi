 using Domain.Model.Entity;
using iRentApi.Controllers.Contract;
using iRentApi.DTO;
using iRentApi.Model.Entity;
using iRentApi.Service.Contract;
using Microsoft.AspNetCore.Mvc;

namespace iRentApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WarehouseController : CrudController<Warehouse, WarehouseDTO, WarehouseDTO, WarehouseDTO>
    {
        public WarehouseController(IServiceWrapper serviceWrapper) : base(serviceWrapper)
        {
        }

        [HttpGet("owner/{id}")]
        public async Task<ActionResult<IEnumerable<WarehouseDTO>>> GetOwnerWarehouse([FromQuery] SelectOptions? options, [FromRoute(Name = "id")] long userId)
        {
            return await Service.WarehouseService.GetOwnerWarehouseList(userId, options);
        }

        [HttpGet("renter/{id}")]
        public async Task<ActionResult<IEnumerable<WarehouseDTO>>> GetRenterWarehouse([FromQuery] SelectOptions? options, [FromRoute(Name = "id")] long userId)
        {
            return await Service.WarehouseService.GetRenterWarehouseList(userId, options);
        }
    }
}