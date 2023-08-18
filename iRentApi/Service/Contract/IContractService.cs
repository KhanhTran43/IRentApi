using AutoMapper;
using Data.Context;
using iRentApi.Helpers;
using iRentApi.Model.Entity;
using Microsoft.Extensions.Options;

namespace iRentApi.Service.Contract
{
    public abstract class IContractService : IRentCRUDService<ContractModel>
    {
        protected IContractService(IRentContext context, IMapper mapper, IOptions<AppSettings> appSettings) : base(context, mapper, appSettings)
        {
        }
    }
}
