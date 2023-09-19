using AutoMapper;
using Data.Context;
using iRentApi.Helpers;
using iRentApi.Service.Database.Contract;
using Microsoft.Extensions.Options;

namespace iRentApi.Service.Database.Implement
{
    public class ContractService : IContractService
    {
        public ContractService(IRentContext context, IMapper mapper, IOptions<AppSettings> appSettings) : base(context, mapper, appSettings)
        {
        }
    }
}
