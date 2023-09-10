using iRentApi.Model.Http.Payment;
using Microsoft.AspNetCore.Mvc;
using Stripe;

namespace iRentApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        [HttpPost]
        public async Task<PaymentIntentResponse> GetPaymentIntent(PaymentIntentRequest request)
        {
            var services = new PaymentIntentService();
            var opt = new PaymentIntentCreateOptions()
            {
                Amount = request.Amount * 1000,
                Currency = "VND",
                AutomaticPaymentMethods = new PaymentIntentAutomaticPaymentMethodsOptions()
                {
                    Enabled = true,
                }
                
            };
            var paymentIntent = await services.CreateAsync(opt);

            return new PaymentIntentResponse() { ClientSecret = paymentIntent.ClientSecret };
        }

        [HttpPost("fee")]
        public async Task<PaymentIntentResponse> GetPaymentIntentWithFee(PaymentIntentRequest request)
        {
            var services = new PaymentIntentService();
            var opt = new PaymentIntentCreateOptions()
            {
                Amount = request.Amount * 1000,
                Currency = "VND",
                AutomaticPaymentMethods = new PaymentIntentAutomaticPaymentMethodsOptions()
                {
                    Enabled = true,
                }

            };
            var paymentIntent = await services.CreateAsync(opt);

            return new PaymentIntentResponse() { ClientSecret = paymentIntent.ClientSecret };
        }

        [HttpPost("create")]
        public async Task<Account> CreateConnectedAccount()
        {
            var accountService = new AccountService();

            var account = await accountService.CreateAsync(new AccountCreateOptions()
            {
                Type = "express",
                Country = "US",
                Email = "test@email.com",
                BusinessType = "individual",
                Individual = new AccountIndividualOptions()
                {
                    FirstName = "Ho",
                    LastName = "Huu Tinh",
                    Dob = new DobOptions() { Day = 17, Month = 10, Year = 2001 },
                    SsnLast4 = "0000",
                    Email = "huutinh@gmail.com",
                    Phone = "000 000 0000",
                    Address = new AddressOptions()
                    {
                        City = "Scaramento",
                        Country = "US",
                        PostalCode = "90002",
                        State = "California",
                        Line1 = "address_full_match",
                    }
                },
                BusinessProfile = new AccountBusinessProfileOptions()
                {
                    Mcc = "4225",
                    Url = "https://irent.com"
                },
                Capabilities = new AccountCapabilitiesOptions()
                {
                    Transfers = new AccountCapabilitiesTransfersOptions() { Requested = true },
                    CardPayments = new AccountCapabilitiesCardPaymentsOptions() { Requested = true },
                },
                ExternalAccount = new AnyOf<string, AccountBankAccountOptions, AccountCardOptions>(
                    new AccountBankAccountOptions()
                    {
                        RoutingNumber = "110000000",
                        AccountNumber = "000123456789",
                        Country = "US"
                    }
                ),
                TosAcceptance = new AccountTosAcceptanceOptions()
                {
                    Date = DateTime.Now,
                    ServiceAgreement = "full",
                    Ip = "8.8.8.8",
                }
            });

            //var accountLinkService = new AccountLinkService();

            //var accountLink = await accountLinkService.CreateAsync(new AccountLinkCreateOptions() { Account = account.Id, ReturnUrl = "http://localhost:4200/login", RefreshUrl = "http://localhost:4200/home", Type = "account_onboarding" });

            //return accountLink;

            return account;
        }

        [HttpPatch("accept/{id}")]
        public async Task<Account> AcceptServiceTerm(string id)
        {
            var options = new AccountUpdateOptions
            {
                TosAcceptance = new AccountTosAcceptanceOptions
                {
                    Date = DateTimeOffset.FromUnixTimeSeconds(1609798905).UtcDateTime,
                    Ip = "8.8.8.8",
                },
            };
            var service = new AccountService();
            return await service.UpdateAsync(id, options);
        }
    }
}
