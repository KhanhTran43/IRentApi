using iRentApi.Controllers.Contract;
using iRentApi.DTO;
using iRentApi.Model.Entity;
using iRentApi.Service.Contract;

namespace iRentApi.Controllers
{
    public class CommentController : CrudController<Comment, CommentDTO, CommentDTO, CommentDTO>
    {
        public CommentController(IServiceWrapper serviceWrapper) : base(serviceWrapper)
        {
        }
    }
}
