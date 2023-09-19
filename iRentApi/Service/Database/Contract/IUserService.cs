using AutoMapper;
using Data.Context;
using Domain.Model.Entity;
using iRentApi.Helpers;
using iRentApi.Model.Entity;
using iRentApi.Model.Service.Stripe;
using Microsoft.Extensions.Options;

namespace iRentApi.Service.Database.Contract
{
    public abstract class IUserService : IRentCRUDService<User>
    {
        protected IUserService(IRentContext context, IMapper mapper, IOptions<AppSettings> appSettings) : base(context, mapper, appSettings)
        {
        }

        public abstract GetIntentPaymentPartiesResult GetIntentPaymentParties(long userId, long ownerId);
    }
}
