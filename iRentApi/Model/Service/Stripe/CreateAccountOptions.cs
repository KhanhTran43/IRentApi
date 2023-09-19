using Stripe;

namespace iRentApi.Model.Service.Stripe
{
    public class CreateAccountOptions
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DobOptions Dob { get; set; }
        public string Email { get; set; }
    }
}
