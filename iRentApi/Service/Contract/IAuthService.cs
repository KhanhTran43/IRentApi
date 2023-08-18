﻿using AutoMapper;
using Data.Context;
using Domain.Model.Entity;
using iRentApi.Helpers;
using iRentApi.Model.Http.Auth;
using iRentApi.Service.Implement;
using Microsoft.Extensions.Options;

namespace iRentApi.Service.Contract
{
    public abstract class IAuthService : IRentService
    {
        protected IAuthService(IRentContext context, IMapper mapper, IOptions<AppSettings> appSettings) : base(context, mapper, appSettings)
        {
        }

        public abstract Task<AuthenticateResponse?> Login(string email, string password);
        public abstract AuthenticateResponse? RefreshToken(string token);
    }
}
