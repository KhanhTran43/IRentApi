using iRentApi.Controllers.Contract;
using iRentApi.DTO;
using iRentApi.Model.Entity;
using iRentApi.Service.Contract;
using Microsoft.AspNetCore.Mvc;

namespace iRentApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : CrudController<Comment, CommentDTO, CommentDTO, CommentDTO>
    {
        public CommentController(IServiceWrapper serviceWrapper) : base(serviceWrapper)
        {
        }
    }
}
