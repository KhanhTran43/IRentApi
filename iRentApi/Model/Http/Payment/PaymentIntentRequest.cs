namespace iRentApi.Model.Http.Payment
{
    public class PaymentIntentRequest
    {
        public int Amount { get; set; }
        public long? UserId { get; set; }
        public long? OwnerId { get; set;}
    }
}
