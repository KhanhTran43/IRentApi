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

        public ServiceWrapper(IRentContext context, IMapper mapper, IOptions<AppSettings> options)
        {
            _userService = new UserService(context, mapper, options);
            _authService = new AuthService(context, mapper, options);
            _warehouseService = new WarehouseService(context, mapper, options);
        }

        public IUserService UserService => _userService;
        public IAuthService AuthService => _authService;
        public IWarehouseService WarehouseService => _warehouseService;

        IUserService IServiceWrapper.UserService => throw new NotImplementedException();

        public IEntityCRUDService<TEntity> EntityService<TEntity>() 
            where TEntity : EntityBase 
        {
            var properties = this.GetType().GetProperties();

            PropertyInfo? servicePropertyInfo = properties.FirstOrDefault(p =>
            {
                var propertyType = p.PropertyType;
                var entityCrudServiceInterfaceType = propertyType.GetInterface("IEntityCRUDService`1");

                if(entityCrudServiceInterfaceType != null)
                {
                    Type[] genericArgumentTypes = entityCrudServiceInterfaceType.GetGenericArguments();
                    Type entityType = typeof(TEntity);
                    Type entityGenericArgumentType = genericArgumentTypes[0];
                    bool result = entityType == entityGenericArgumentType;
                    return result;
                }

                return false;
            });
            if (servicePropertyInfo != null) return (IEntityCRUDService<TEntity>)servicePropertyInfo.GetValue(this);
            else throw new Exception($"Service of type {typeof(TEntity).Name} does not exist");
        }

    }
}
