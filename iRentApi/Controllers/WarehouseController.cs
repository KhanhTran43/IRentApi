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

        [HttpGet("user/{id}")]
        public async Task<ActionResult<IEnumerable<WarehouseDTO>>> GetOwnWarehouse([FromQuery] SelectOptions? options, [FromRoute(Name = "id")] long userId)
        {
            return await Service.WarehouseService.GetOwnWarehouseList(userId, options);
        }
    }
}