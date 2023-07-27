using iRentApi.Controllers.Contract;
using iRentApi.Service.Contract;
using Microsoft.AspNetCore.Mvc;

namespace iRentApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : IRentController
    {
        public AuthController(IServiceWrapper serviceWrapper) : base(serviceWrapper)
        {
        }
    }
}
