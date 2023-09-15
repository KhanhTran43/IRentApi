using AutoMapper;
using Data.Context;
using Domain.Model.Entity;
using iRentApi.DTO;
using iRentApi.DTO.Contract;
using iRentApi.Helpers;
using iRentApi.Model.Entity.Contract;
using iRentApi.Service.Contract;
using Microsoft.Extensions.Options;
using System.Reflection;

namespace iRentApi.Service.Implement
{
    public class ServiceWrapper : IServiceWrapper
    {
        private readonly IUserService _userService;
        private readonly IAuthService _authService;
        private readonly IWarehouseService _warehouseService;
        private readonly ICommentService _commentService;
        private readonly IContractService _contractService;
        private readonly IPostService _postService;
        private readonly IRentedWarehouseService _rentedWarehouseService;
        private readonly StripeService _stripeService;

        public ServiceWrapper(IRentContext context, IMapper mapper, IOptions<AppSettings> options)
        {
            _userService = new UserService(context, mapper, options);
            _authService = new AuthService(context, mapper, options);
            _warehouseService = new WarehouseService(context, mapper, options);
            _commentService = new CommentService(context, mapper, options);
            _contractService = new ContractService(context, mapper, options);
            _postService = new PostService(context, mapper, options);
            _rentedWarehouseService = new RentedWarehouseService(context, mapper, options);
            _stripeService = new StripeService(context, mapper, options);
        }

        public IUserService UserService => _userService;
        public IAuthService AuthService => _authService;
        public IWarehouseService WarehouseService => _warehouseService;

        public ICommentService CommentService => _commentService;

        public IContractService ContractService => _contractService;

        public IPostService PostService => _postService;

        public IRentedWarehouseService RentedWarehouseService => _rentedWarehouseService;

        public StripeService StripeService => _stripeService;

        public IRentCRUDService<TEntity> EntityService<TEntity>() 
            where TEntity : EntityBase 
        {
            var properties = this.GetType().GetProperties();

            PropertyInfo? servicePropertyInfo = properties.FirstOrDefault(p =>
            {
                var propertyType = p.PropertyType;
                var entityCrudServiceBaseType = propertyType.BaseType;

                if(entityCrudServiceBaseType != null && entityCrudServiceBaseType.Equals(typeof(IRentCRUDService<TEntity>)))
                {
                    Type[] genericArgumentTypes = entityCrudServiceBaseType.GetGenericArguments();
                    Type entityType = typeof(TEntity);
                    Type entityGenericArgumentType = genericArgumentTypes[0];
                    bool result = entityType == entityGenericArgumentType;
                    return result;
                }

                return false;
            });
            if (servicePropertyInfo != null) return (IRentCRUDService<TEntity>)servicePropertyInfo.GetValue(this);
            else throw new Exception($"Service of type {typeof(TEntity).Name} does not exist");
        }
    }
}
