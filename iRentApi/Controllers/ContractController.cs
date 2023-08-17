using iRentApi.Controllers.Contract;
using iRentApi.DTO;
using iRentApi.Model.Entity;
using iRentApi.Service.Contract;
using Microsoft.AspNetCore.Mvc;

namespace iRentApi.Controllers
{
    public class ContractController : CrudController<ContractModel, ContractDTO, ContractDTO, ContractDTO>
    {
        public ContractController(IServiceWrapper serviceWrapper) : base(serviceWrapper) { }
    }
}
