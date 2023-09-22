using iRentApi.Controllers.Contract;
using iRentApi.DTO;
using iRentApi.Model.Entity;
using iRentApi.Model.Service.Crud;
using iRentApi.Service.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace iRentApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RentedWarehouseController : CrudController<RentedWarehouseInfo, RentedWarehouseDTO, CreateRentedWarehouseDTO, RentedWarehouseDTO>
    {
        public RentedWarehouseController(IUnitOfWork serviceWrapper) : base(serviceWrapper)
        {
        }

        [HttpGet("{warehouseId}/rented")]
        public async Task<ActionResult<bool>> CheckWarehouseRented([FromRoute] long warehouseId)
        {
            return await Service.RentedWarehouseService.CheckWarehouseRented(warehouseId);
        }

        [HttpPost("renter/{id}")]
        public async Task<ActionResult<IEnumerable<RentedWarehouseDTO>>> GetRenterWarehouse
        (
            [FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Allow)] GetStaticRequest? options,
            [FromRoute(Name = "id")] long userId
        )
        {
            return await Service.RentedWarehouseService.GetRenterWarehouseList(userId, options);
        }

        [HttpPatch("confirm/{id}")]
        public async Task<IActionResult> ConfirmRentedWarehouse([FromRoute(Name = "id")] long rentedWarehouseId)
        {
            try
            {
                await Service.RentedWarehouseService.Confirm(rentedWarehouseId);
                return Ok("Confirmed");
            } 
            catch (Exception ex) {
                return Problem(ex.Message);
            }
        }
    }
}
