using AutoMapper;
using Data.Context;
using iRentApi.Helpers;
using iRentApi.Model.Service.Stripe;
using iRentApi.Service.Database.Implement;
using Microsoft.Extensions.Options;
using Stripe;

namespace iRentApi.Service.Stripe
{
    public class StripeService
    {
        public StripeService()
        {
        }

        public CreateAccountResult CreateStripeAccount(string paymentMethod, CreateAccountOptions options)
        {
            var accountService = new AccountService();

            var account = accountService.Create(new AccountCreateOptions()
            {
                Type = "custom",
                Country = "US",
                Email = options.Email,
                BusinessType = "individual",
                Individual = new AccountIndividualOptions()
                {
                    FirstName = options.FirstName,
                    LastName = options.LastName,
                    Dob = options.Dob,
                    SsnLast4 = "0000",
                    Email = options.Email,
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
                Name = options.FirstName,
                Email = options.Email,
                PaymentMethod = paymentMethod,
            });

            // Set default payment method
            var customerUpdateOptions = new CustomerUpdateOptions
            {
                InvoiceSettings = new CustomerInvoiceSettingsOptions() { DefaultPaymentMethod = paymentMethod },
            };

            customerService.UpdateAsync(customer.Id, customerUpdateOptions);

            return new CreateAccountResult() { AccountId = account.Id, CustomerId = customer.Id };
        }
        public string Refund(string paymentItent)
        {
            var paymentIntentService = new PaymentIntentService();
            var paymentIntent = paymentIntentService.Get(paymentItent);

            string transferGroup = paymentIntent.TransferGroup;

            var transferOptions = new TransferListOptions
            {
                TransferGroup = transferGroup,
            };

            var transferService = new TransferService();
            var transfers = transferService.List(transferOptions);

            var transfer = transfers.ToList().First();

            var refundAmount = transfer.Amount / 3;

            var options = new TransferReversalCreateOptions
            {
                Amount = refundAmount,
                Description = "Refund for Transfer"
            };

            var service = new TransferReversalService();
            TransferReversal reversal = service.Create(transfer.Id, options);

            return reversal.Id;
        }
    }
}
