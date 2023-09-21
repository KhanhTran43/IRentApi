using Domain.Model.Entity;
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
    public class WarehouseController : CrudController<Warehouse, WarehouseDTO, CreateWarehouseDTO, WarehouseDTO>
    {
        public WarehouseController(IUnitOfWork serviceWrapper) : base(serviceWrapper)
        {
        }

        [HttpPost("owner/{id}")]
        public async Task<ActionResult<IEnumerable<WarehouseDTO>>> GetOwnerWarehouse
        (
            [FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Allow)] GetStaticRequest? options,
            [FromRoute(Name = "id")] long ownerId
        )
        {
            return await Service.WarehouseService.GetOwnerWarehouseList(ownerId, options);
        }

        [HttpPost("{warehouseId}/comment/{userId}")]
        public async Task<ActionResult<WarehouseCommentDTO>> AddComment([FromRoute] long warehouseId, [FromRoute] long userId, [FromBody] CreateWarehouseCommentDTO commentDTO)
        {
            var addedComment = await Service.WarehouseService.AddComment<WarehouseCommentDTO>(warehouseId, userId, commentDTO);
            return addedComment;
        }
    }
}