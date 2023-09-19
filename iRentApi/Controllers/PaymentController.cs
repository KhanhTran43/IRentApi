using iRentApi.Controllers.Contract;
using iRentApi.Model.Http.Payment;
using iRentApi.Model.Service.Stripe;
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
        public async Task<ActionResult<PaymentIntentResponse>> GetPaymentIntentWithFee(PaymentIntentRequest request)
        {
            var services = new PaymentIntentService();
            var fee = request.Amount * 0.02;

            if (request.OwnerId == null || request.UserId == null) return BadRequest("ownerId or userId value is invalid");

            var parties = Service.UserService.GetIntentPaymentParties(request.UserId.Value, request.OwnerId.Value);

            var customerService = new CustomerService();
            var customer = customerService.Get(parties.CustomerId);

            var opt = new PaymentIntentCreateOptions()
            {
                Amount = request.Amount * 1000,
                Currency = "VND",
                Customer = customer.Id,
                PaymentMethod = customer.InvoiceSettings.DefaultPaymentMethodId,
                ApplicationFeeAmount = (long)fee * 1000,
                PaymentMethodTypes = new List<string> { "card" },
                TransferData = new PaymentIntentTransferDataOptions()
                {
                    Destination = parties.AccountId,
                },
            };
            var paymentIntent = await services.CreateAsync(opt);

            return new PaymentIntentResponse() { ClientSecret = paymentIntent.ClientSecret };
        }
    }
}
