using iRentApi.Controllers.Contract;
using iRentApi.DTO;
using iRentApi.Model.Entity;
using iRentApi.Service.Database;
using Microsoft.AspNetCore.Mvc;

namespace iRentApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : CrudController<Warehouse, PostDTO, PostDTO, PostDTO>
    {
        public PostController(IUnitOfWork serviceWrapper) : base(serviceWrapper)
        {
        }
    }
}
