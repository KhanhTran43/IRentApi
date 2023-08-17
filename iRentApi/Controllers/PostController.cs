using iRentApi.Controllers.Contract;
using iRentApi.DTO;
using iRentApi.Model.Entity;
using iRentApi.Service.Contract;
using Microsoft.AspNetCore.Mvc;

namespace iRentApi.Controllers
{
    public class PostController : CrudController<Post, PostDTO, PostDTO, PostDTO>
    {
        public PostController(IServiceWrapper serviceWrapper) : base(serviceWrapper)
        {
        }
    }
}
