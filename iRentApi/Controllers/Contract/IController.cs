using iRentApi.Service.Database;
using Microsoft.AspNetCore.Mvc;

namespace iRentApi.Controllers.Contract
{
    public abstract class IController : ControllerBase
    {
        protected abstract IUnitOfWork Service { get; }
    }
}
