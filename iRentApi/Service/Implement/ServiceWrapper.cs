using AutoMapper;
using Data.Context;
using Domain.Model.Entity;
using iRentApi.DTO;
using iRentApi.DTO.Contract;
using iRentApi.Helpers;
using iRentApi.Model.Entity.Contract;
using iRentApi.Service.Contract;
using Microsoft.Extensions.Options;

namespace iRentApi.Service.Implement
{
    public class ServiceWrapper : IServiceWrapper
    {
        private readonly IUserService _userService;
        private readonly IAuthService _authService;

        public ServiceWrapper(IRentContext context, IMapper mapper, IOptions<AppSettings> options)
        {
            _userService = new UserService(context, mapper, options);
            _authService = new AuthService(context, mapper, options);
        }

        public IUserService UserService => _userService;
        public IAuthService AuthService => _authService;

        IUserService IServiceWrapper.UserService => throw new NotImplementedException();

        public IEntityCRUDService<TEntity> EntityService<TEntity>() 
            where TEntity : EntityBase 
        {

            return (IEntityCRUDService<TEntity>)_userService;
        }

    }
}
