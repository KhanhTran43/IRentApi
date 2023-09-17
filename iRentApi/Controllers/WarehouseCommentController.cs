using iRentApi.Controllers.Contract;
using iRentApi.DTO;
using iRentApi.Model.Entity;
using iRentApi.Service.Contract;
using Microsoft.AspNetCore.Mvc;

namespace iRentApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WarehouseCommentController : CrudController<WarehouseComment, WarehouseCommentDTO, WarehouseCommentDTO, WarehouseCommentDTO>
    {
        public WarehouseCommentController(IServiceWrapper serviceWrapper) : base(serviceWrapper)
        {
        }
    }
}
