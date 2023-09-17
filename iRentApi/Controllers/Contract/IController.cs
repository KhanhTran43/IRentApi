using iRentApi.Service.Contract;
using iRentApi.Service.Implement;
using Microsoft.AspNetCore.Mvc;

namespace iRentApi.Controllers.Contract
{
    public abstract class IController : ControllerBase
    {
        protected abstract IServiceWrapper Service { get; }
    }
}
