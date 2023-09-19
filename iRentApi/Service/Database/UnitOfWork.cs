using AutoMapper;
using Data.Context;
using Domain.Model.Entity;
using iRentApi.DTO;
using iRentApi.DTO.Contract;
using iRentApi.Helpers;
using iRentApi.Model.Entity.Contract;
using iRentApi.Service.Database.Contract;
using iRentApi.Service.Database.Implement;
using iRentApi.Service.Stripe;
using Microsoft.Extensions.Options;
using System.Reflection;

namespace iRentApi.Service.Database
{
    public class UnitOfWork : IUnitOfWork
    {
        readonly IRentContext _context;
        readonly IMapper _mapper;
        readonly IOptions<AppSettings> _options;
        readonly StripeService _stripeService;

        private IUserService _userService;
        private IAuthService _authService;
        private IWarehouseService _warehouseService;
        private ICommentService _commentService;
        private IContractService _contractService;
        private IPostService _postService;
        private IRentedWarehouseService _rentedWarehouseService;

        public UnitOfWork(IRentContext context, IMapper mapper, IOptions<AppSettings> options, StripeService stripeService)
        {
            _context = context;
            _mapper = mapper;
            _options = options;
            _stripeService = stripeService;
        }

        public IUserService UserService
        {
            get 
            { 
                return _userService ??= new UserService(_context, _mapper, _options, _stripeService); ;
            } 
        }
        public IAuthService AuthService
        {
            get
            {
                return _authService ??= new AuthService(_context, _mapper, _options); ;
            }
        }
        public IWarehouseService WarehouseService
        {
            get
            {
                return _warehouseService ??= new WarehouseService(_context, _mapper, _options);
            }
        }

        public ICommentService CommentService
        {
            get
            {
                return _commentService ??= new CommentService(_context, _mapper, _options); ;
            }
        }

        public IContractService ContractService
        { 
            get
            {
                return _contractService ??= new ContractService(_context, _mapper, _options); ;
            }
        }

        public IPostService PostService
        {
            get
            {
                return _postService ??= new PostService(_context, _mapper, _options); ;
            }
        }

        public IRentedWarehouseService RentedWarehouseService
        {
            get
            {
                return _rentedWarehouseService ??= new RentedWarehouseService(_context, _mapper, _options); ;
            }
        }


        public IRentCRUDService<TEntity> EntityService<TEntity>()
            where TEntity : EntityBase
        {
            var properties = GetType().GetProperties();

            PropertyInfo? servicePropertyInfo = properties.FirstOrDefault(p =>
            {
                var propertyType = p.PropertyType;
                var entityCrudServiceBaseType = propertyType.BaseType;

                if (entityCrudServiceBaseType != null && entityCrudServiceBaseType.Equals(typeof(IRentCRUDService<TEntity>)))
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
            else throw new System.Exception($"Service of type {typeof(TEntity).Name} does not exist");
        }
    }
}
