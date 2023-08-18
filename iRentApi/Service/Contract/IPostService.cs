using AutoMapper;
using Data.Context;
using iRentApi.Helpers;
using iRentApi.Model.Entity;
using Microsoft.Extensions.Options;

namespace iRentApi.Service.Contract
{
    public abstract class IPostService : IRentCRUDService<Post>
    {
        protected IPostService(IRentContext context, IMapper mapper, IOptions<AppSettings> appSettings) : base(context, mapper, appSettings)
        {
        }
    }
}
