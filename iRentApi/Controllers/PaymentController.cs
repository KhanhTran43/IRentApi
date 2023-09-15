using iRentApi.Controllers.Contract;
using iRentApi.Model.Http.Payment;
using iRentApi.Model.Service.Stripe;
using iRentApi.Service.Contract;
using Microsoft.AspNetCore.Mvc;
using Stripe;

namespace iRentApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : IRentController
    {
        public PaymentController(IServiceWrapper serviceWrapper) : base(serviceWrapper)
        {
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

            var parties = Service.StripeService.GetIntentPaymentParties(request.UserId.Value, request.OwnerId.Value);

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

        [HttpPost("account/{paymentMethod}")]
        public async Task<ActionResult<CreateAccountResult>> CreateConnectedAccount(string paymentMethod)
        {
            return Ok(Service.StripeService.CreateStripeAccount(paymentMethod));
        }

        [HttpPost("attach-payment-method")]
        public IActionResult AttachPaymentMethod([FromBody] AttachPaymentMethodRequest attachPaymentMethodRequest)
        {
            try
            {
                // Attach the PaymentMethod to the connected account
                var paymentMethodService = new PaymentMethodService();
                var attachedPaymentMethod = paymentMethodService.Attach(
                    attachPaymentMethodRequest.PaymentMethodId,
                    new PaymentMethodAttachOptions()
                    {
                        Customer = "cus_OdAPzwnTLlT8CI"
                    },
                    requestOptions: new RequestOptions
                    {
                        StripeAccount = attachPaymentMethodRequest.AccountId
                    }
                );

                // Handle success
                return Ok("PaymentMethod attached to connected account");
            }
            catch (StripeException e)
            {
                // Handle error
                return BadRequest($"Error: {e.Message}");
            }
        }
    }
}
