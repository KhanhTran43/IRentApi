namespace iRentApi.DTO
{
    public class RentedInfo
    {
        public long? RenterId { get; set; }
        public DateTime RentedDate { get; set; }
        public DateTime EndDate { get; set; }
        public string ContractBase64 { get; set; }
    }
}
