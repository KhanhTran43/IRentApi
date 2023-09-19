using Domain.Model.Entity;
using iRentApi.Controllers.Contract;
using iRentApi.DTO;
using iRentApi.Service.Database;
using Microsoft.AspNetCore.Mvc;

namespace iRentApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : CrudController<User, UserDTO, CreateUserDTO, UserDTO>
    {
        public UserController(IUnitOfWork serviceWrapper) : base(serviceWrapper)
        {
        }
    }
}
