using AutoMapper;
using Data.Context;
using Domain.Model.Entity;
using iRentApi.DTO;
using iRentApi.DTO.Contract;
using iRentApi.Model.Entity.Contract;
using iRentApi.Service.Contract;

namespace iRentApi.Service.Implement
{
    public class ServiceWrapper : IServiceWrapper
    {
        private readonly IUserService _userService;

        public ServiceWrapper(IRentContext context, IMapper mapper)
        {
            _userService = new UserService(context, mapper);
        }

        public IUserService UserService => _userService;

        public IEntityCRUDService<TEntity> EntityService<TEntity>() 
            where TEntity : EntityBase 
        {

            return (IEntityCRUDService<TEntity>)_userService;
        }

    }
}
