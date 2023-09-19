using iRentApi.Controllers.Contract;
using iRentApi.DTO;
using iRentApi.Model.Entity;
using iRentApi.Service.Database;
using Microsoft.AspNetCore.Mvc;

namespace iRentApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContractController : CrudController<ContractModel, ContractDTO, ContractDTO, ContractDTO>
    {
        public ContractController(IUnitOfWork serviceWrapper) : base(serviceWrapper) { }
    }
}
