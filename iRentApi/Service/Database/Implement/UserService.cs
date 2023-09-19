using AutoMapper;
using Data.Context;
using Domain.Model.Entity;
using iRentApi.DTO;
using iRentApi.DTO.Contract;
using iRentApi.Helpers;
using iRentApi.Model.Service.Stripe;
using iRentApi.Service.Database.Contract;
using iRentApi.Service.Stripe;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Options;
using Stripe;

namespace iRentApi.Service.Database.Implement
{
    public class UserService : IUserService
    {
        public UserService(IRentContext context, IMapper mapper, IOptions<AppSettings> appSettings, StripeService stripeService) : base(context, mapper, appSettings)
        {
            StripeService = stripeService;
        }

        StripeService StripeService { get; }

        public override EntityEntry<User> Insert(IInsertDTO<User> insert)
        {
            var insertDTO = (insert as CreateUserDTO);
            var entityEntry = base.Insert(insertDTO);
            var entity = entityEntry.Entity;
            var dob = insertDTO.Dob;

            var options = new CreateAccountOptions() { 
                FirstName = insertDTO.Name,
                LastName = "Owner",
                Dob = new DobOptions() { Day = dob.Day, Month = dob.Month, Year = dob.Year },
                Email = insertDTO.Email,
            };

            var createAccountResult = StripeService.CreateStripeAccount(insertDTO.PaymentMethod, options);

            entity.AccountId = createAccountResult.AccountId;
            entity.CustomerId = createAccountResult.CustomerId;

            return entityEntry;
        }

        public override GetIntentPaymentPartiesResult GetIntentPaymentParties(long userId, long ownerId)
        {
            var customerId = this.Context.Users.FirstOrDefault(u => u.Id == userId)?.CustomerId;
            var accountId = this.Context.Users.FirstOrDefault(u => u.Id == ownerId)?.AccountId;

            return new GetIntentPaymentPartiesResult() { AccountId = accountId, CustomerId = customerId };
        }
    }
}
