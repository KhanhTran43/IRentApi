using iRentApi.DTO.Contract;
using iRentApi.Model.Entity;

namespace iRentApi.DTO
{
    public interface ICreateExtendRentingDTO : IInsertDTO<RentingExtend>
    {
        public string NewContractBase64 { get; set; }
        public DateTime NewEndDate { get; set; }
        public string Hash { get; set; }
        public decimal Total { get; set; }
    }

    public class CreateExtendRentingDTO : ICreateExtendRentingDTO
    {
        public int Duration { get; set; }
        public DateTime ExtendDate { get; set; }
        public decimal Total { get; set; }
        public string NewContractBase64 { get; set; }
        public DateTime NewEndDate { get; set; }
        public string Hash { get; set; }
    }
}
