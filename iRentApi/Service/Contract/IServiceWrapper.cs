﻿using Domain.Model.Entity;
using iRentApi.DTO.Contract;
using iRentApi.Model.Entity.Contract;

namespace iRentApi.Service.Contract
{
    public interface IServiceWrapper
    {
        IUserService UserService { get; }
        IAuthService AuthService { get; }

        public IEntityCRUDService<TEntity> EntityService<TEntity>()
            where TEntity : EntityBase;
    }
}