using iRentApi.DTO;

namespace iRentApi.Model.Http.RentedWarehouse
{
    public class VerifiedContractResponse
    {
        public RentedWarehouseDTO RentedWarehouse { get; set; }
        public bool IsValid { get; set; }
    }
}
