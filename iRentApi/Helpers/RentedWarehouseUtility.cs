using iRentApi.Model.Entity;

namespace iRentApi.Helpers
{
    public static class RentedWarehouseUtility
    {
        public static bool IsRentingStatus(RentedWarehouseStatus status)
        {
            return status == RentedWarehouseStatus.Waiting
                || status == RentedWarehouseStatus.Renting
                || status == RentedWarehouseStatus.Canceling
                || status == RentedWarehouseStatus.Confirmed;
        }

        public static List<RentedWarehouseStatus> notRentingStatuses = new()
        {
            RentedWarehouseStatus.Expired,
            RentedWarehouseStatus.Ended,
            RentedWarehouseStatus.Canceled
        };

        public static bool IsNotRentingStatus(RentedWarehouseStatus status)
        {
            return status == RentedWarehouseStatus.Expired
                || status == RentedWarehouseStatus.Ended
                || status == RentedWarehouseStatus.Canceled;
        }
    }
}
