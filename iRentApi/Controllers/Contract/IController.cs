using iRentApi.Service.Contract;
using iRentApi.Service.Implement;
using Microsoft.AspNetCore.Mvc;

namespace iRentApi.Controllers.Contract
{
    public interface IController
    {
        IServiceWrapper Service { get; }

        [NonAction]
        ActionResult BadRequestResult(object? messsage = null);

        [NonAction]
        ActionResult NotFoundResult(string? messsage = null);
    }
}
