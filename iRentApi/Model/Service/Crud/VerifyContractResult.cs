using iRentApi.Model.Entity;

namespace iRentApi.Model.Service.Crud
{
    public class VerifyContractResult
    {
        public RentedWarehouseInfo? RentedWarehouseInfo { get; set; }
        public bool IsValid { get; set; } = false;
    }
}
