using Domain.Model.Entity;
using iRentApi.DTO.Contract;
using iRentApi.Model.Entity.Contract;
using iRentApi.Service.Database.Contract;
using iRentApi.Service.Stripe;

namespace iRentApi.Service.Database
{
    public interface IUnitOfWork
    {
        IUserService UserService { get; }
        IAuthService AuthService { get; }
        IWarehouseService WarehouseService { get; }
        ICommentService CommentService { get; }
        IContractService ContractService { get; }
        IPostService PostService { get; }
        IRentedWarehouseService RentedWarehouseService { get; }
        public IRentCRUDService<TEntity> EntityService<TEntity>()
            where TEntity : EntityBase;
    }
}
