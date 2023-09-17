using iRentApi.Service.Contract;
using Microsoft.AspNetCore.Mvc;

namespace iRentApi.Controllers.Contract
{
    public abstract class IRentController : IController
    {
        private IServiceWrapper _serviceWrapper;

        public IRentController(IServiceWrapper serviceWrapper)
        {
            _serviceWrapper = serviceWrapper;
        }

        protected override IServiceWrapper Service => _serviceWrapper;
        protected Dictionary<string, Action<object>> ResolveActions;

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
