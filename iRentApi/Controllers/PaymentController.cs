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
    }
}
