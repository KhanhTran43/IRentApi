using Domain.Model.Entity;
using iRentApi.Controllers.Contract;
using iRentApi.DTO;
using iRentApi.DTO.Contract;
using iRentApi.Service.Contract;
using Microsoft.AspNetCore.Mvc;

namespace iRentApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : CrudController<User, UserDTO, UserDTO, UserDTO>
    {
        public UserController(IServiceWrapper serviceWrapper) : base(serviceWrapper)
        {
        }
    }
}
