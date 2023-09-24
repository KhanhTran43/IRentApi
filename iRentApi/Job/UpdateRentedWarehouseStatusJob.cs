using iRentApi.Service.Database;

namespace iRentApi.Job
{
    public class UpdateRentedWarehouseStatusJob
    {
        private readonly IUnitOfWork _unitOfWork;
        public UpdateRentedWarehouseStatusJob(IUnitOfWork unitOfWork) {
            _unitOfWork = unitOfWork;
        }

        public async Task Execute()
        {
            try
            {
                await _unitOfWork.RentedWarehouseService.ResolveAllStatus();
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.ToString());
            }
        }
    }
}
