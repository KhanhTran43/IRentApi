using iRentApi.Controllers.Contract;
using iRentApi.DTO;
using iRentApi.Model.Entity;
using iRentApi.Service.Contract;
using Microsoft.AspNetCore.Mvc;

namespace iRentApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : CrudController<Warehouse, PostDTO, PostDTO, PostDTO>
    {
        public PostController(IServiceWrapper serviceWrapper) : base(serviceWrapper)
        {
        }
    }
}
