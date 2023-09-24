using iRentApi.Controllers.Contract;
using iRentApi.DTO;
using iRentApi.Model.Entity;
using iRentApi.Model.Service.Crud;
using iRentApi.Service.Database;
using iRentApi.Service.Stripe;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace iRentApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RentedWarehouseController : CrudController<RentedWarehouseInfo, RentedWarehouseDTO, CreateRentedWarehouseDTO, RentedWarehouseDTO>
    {
        StripeService StripeService { get; }
        public RentedWarehouseController(IUnitOfWork serviceWrapper, StripeService stripeService) : base(serviceWrapper)
        {
            StripeService = stripeService;
        }

        [HttpGet("{warehouseId}/rented")]
        public async Task<ActionResult<bool>> CheckWarehouseRented([FromRoute] long warehouseId)
        {
            return await Service.RentedWarehouseService.CheckWarehouseRented(warehouseId);
        }

        [HttpPost("renter/{id}")]
        public async Task<ActionResult<IEnumerable<RentedWarehouseDTO>>> GetRenterRentedWarehouse
        (
            [FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Allow)] GetStaticRequest? options,
            [FromRoute(Name = "id")] long userId
        )
        {
            return await Service.RentedWarehouseService.GetRenterRentedWarehouseList(userId, options);
        }

        [HttpPost("owner/{id}")]
        public async Task<ActionResult<IEnumerable<RentedWarehouseDTO>>> GetOwnerRentedWarehouse
        (
            [FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Allow)] GetStaticRequest? options,
            [FromRoute(Name = "id")] long userId
        )
        {
            return await Service.RentedWarehouseService.GetOwnerRentedWarehouseList(userId, options);
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

        [Authorize(Roles = nameof(Role.Renter))]
        [HttpPatch("cancel_request/{id}")]
        public async Task<IActionResult> RequestCancelRentedWarehouse([FromRoute(Name = "id")] long rentedWarehouseId)
        {
            try
            {
                await Service.RentedWarehouseService.RequestCancel(rentedWarehouseId);
                return Ok("Cancel Requested");
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [Authorize(Roles = nameof(Role.Owner))]
        [HttpPatch("cancel_confirm/{id}")]
        public async Task<IActionResult> ConfirmCanceldWarehouse([FromRoute(Name = "id")] long rentedWarehouseId)
        {
            try
            {
                await Service.RentedWarehouseService.ConfirmCancel(rentedWarehouseId);
                var rentedWarehouseInfo = await Service.RentedWarehouseService.SelectByID(rentedWarehouseId);
                if (rentedWarehouseInfo.DepositPayment != null)
                {
                    var reverseTransfer = StripeService.Refund(rentedWarehouseInfo.DepositPayment);
                    return Ok($"Cancel Confirmed: {reverseTransfer}");
                }
                else
                {
                    return Ok($"Cancel Confirmed");
                } 
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}
