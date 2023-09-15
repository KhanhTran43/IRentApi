using Domain.Model.Entity;
using iRentApi.DTO.Contract;
using iRentApi.Model.Entity.Contract;
using iRentApi.Service.Implement;

namespace iRentApi.Service.Contract
{
    public interface IServiceWrapper
    {
        IUserService UserService { get; }
        IAuthService AuthService { get; }
        IWarehouseService WarehouseService { get; }
        ICommentService CommentService { get; }
        IContractService ContractService { get; }
        IPostService PostService { get; }
        IRentedWarehouseService RentedWarehouseService { get; }
        StripeService StripeService { get; }

        public IRentCRUDService<TEntity> EntityService<TEntity>()
            where TEntity : EntityBase;
    }
}
