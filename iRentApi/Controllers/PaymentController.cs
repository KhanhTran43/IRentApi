using iRentApi.Controllers.Contract;
using iRentApi.Model.Http.Payment;
using iRentApi.Service.Database;
using iRentApi.Service.Stripe;
using Microsoft.AspNetCore.Mvc;
using Stripe;

namespace iRentApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : IRentController
    {
        StripeService StripeService { get; }
        public PaymentController(IUnitOfWork serviceWrapper, StripeService stripeService) : base(serviceWrapper)
        {
            StripeService = stripeService;
        }

        [HttpPost]
        public async Task<ActionResult<PaymentIntentResponse>> GetPaymentIntent(PaymentIntentRequest request)
        {
            var services = new PaymentIntentService();
            var opt = new PaymentIntentCreateOptions()
            {
                Amount = (long)request.Amount * 1000,
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
        public async Task<ActionResult<PaymentIntentResponse>> GetPaymentIntentWithFee(PaymentIntentRequest request)
        {
            var paymentIntentService = new PaymentIntentService();

            if (request.OwnerId == null || request.UserId == null) return BadRequest("ownerId or userId value is invalid");

            var parties = Service.UserService.GetIntentPaymentParties(request.UserId.Value, request.OwnerId.Value);

            var customerService = new CustomerService();
            var customer = customerService.Get(parties.CustomerId);
            var paymentMethod = customer.InvoiceSettings.DefaultPaymentMethodId;
            if (paymentMethod == null)
            {
                var options = new CustomerListPaymentMethodsOptions
                {
                    Type = "card",
                };
                StripeList<PaymentMethod> paymentMethods = customerService.ListPaymentMethods(
                    customer.Id,
                    options);
                if (paymentMethods.Any())
                    paymentMethod = paymentMethods.First().Id;
            }

            long totalAmount = (long)request.Amount * 1000;
            double fee = totalAmount * 0.02;
            double stripePercentageFee = 0.029; // Stripe's percentage fee (2.9%)
            double stripeFixedFee = 0.3; // Stripe's fixed fee in cents (30 cents)
            double usdToVndExchangeRate = 23000; // Current exchange rate from USD to VND (1 USD = 23,000 VND)
            long stripeFixedFeeVND = (long)(stripeFixedFee * usdToVndExchangeRate);

            long stripeFee = (long)((totalAmount * stripePercentageFee) + stripeFixedFeeVND);

            long totalFee = (long)(fee + stripeFee);

            var opt = new PaymentIntentCreateOptions()
            {
                Amount = totalAmount,
                Currency = "VND",
                Customer = customer.Id,
                PaymentMethod = paymentMethod,
                ApplicationFeeAmount = totalFee,
                PaymentMethodTypes = new List<string> { "card" },
                TransferData = new PaymentIntentTransferDataOptions()
                {
                    Destination = parties.AccountId,
                },
            };
            var paymentIntent = await paymentIntentService.CreateAsync(opt);

            return new PaymentIntentResponse() { ClientSecret = paymentIntent.ClientSecret };
        }

        [HttpPost("refund/{id}")]
        public async Task<ActionResult<string>> Refund([FromRoute] string id)
        {
            return StripeService.Refund(id);
        }
    }
}
