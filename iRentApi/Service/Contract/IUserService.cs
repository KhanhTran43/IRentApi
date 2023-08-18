using AutoMapper;
using Data.Context;
using Domain.Model.Entity;
using iRentApi.Helpers;
using iRentApi.Model.Entity;
using Microsoft.Extensions.Options;

namespace iRentApi.Service.Contract
{
    public abstract class IUserService : IRentCRUDService<User>
    {
        protected IUserService(IRentContext context, IMapper mapper, IOptions<AppSettings> appSettings) : base(context, mapper, appSettings)
        {
        }
    }
}
