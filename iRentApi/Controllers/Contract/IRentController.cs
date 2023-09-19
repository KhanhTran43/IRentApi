using iRentApi.Service.Database;
using Microsoft.AspNetCore.Mvc;

namespace iRentApi.Controllers.Contract
{
    public abstract class IRentController : IController
    {
        private IUnitOfWork _serviceWrapper;

        public IRentController(IUnitOfWork serviceWrapper)
        {
            _serviceWrapper = serviceWrapper;
        }

        protected override IUnitOfWork Service => _serviceWrapper;
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
