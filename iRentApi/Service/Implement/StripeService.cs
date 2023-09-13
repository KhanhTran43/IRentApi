using iRentApi.Model.Service.Stripe;
using Stripe;

namespace iRentApi.Service.Implement
{
    public class StripeService
    {
        public StripeCreateAccountResult CreateStripeAccount(string paymentMethod)
        {
            var accountService = new AccountService();

            var account = accountService.Create(new AccountCreateOptions()
            {
                Type = "custom",
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

            var customerService = new CustomerService();
            var customer = customerService.Create(new CustomerCreateOptions()
            {
                Name = "Tran Quoc Khanh Customer",
                Email = "quockhanh@gmail.com",
                PaymentMethod = paymentMethod,
            });

            return new StripeCreateAccountResult() { AccountId = account.Id, CustomerId = customer.Id };
        }
    }
}
