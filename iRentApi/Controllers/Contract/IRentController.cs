using iRentApi.Service.Contract;
using Microsoft.AspNetCore.Mvc;

namespace iRentApi.Controllers.Contract
{
    public abstract class IRentController : ControllerBase, IController
    {
        private IServiceWrapper _serviceWrapper;

        protected IRentController(IServiceWrapper serviceWrapper)
        {
            _serviceWrapper = serviceWrapper;
        }

        public IServiceWrapper Service => _serviceWrapper;

        [NonAction]
        public ActionResult BadRequestResult(object? messsage = null)
        {
            if (messsage == null) return BadRequest();
            else return BadRequest(messsage);
        }

        [NonAction]
        public ActionResult NotFoundResult(string? messsage = null)
        {
            if (messsage == null) return NotFound();
            else return NotFound(messsage);
        }
    }
}
