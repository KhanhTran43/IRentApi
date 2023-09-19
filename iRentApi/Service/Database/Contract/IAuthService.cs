using AutoMapper;
using Data.Context;
using Domain.Model.Entity;
using iRentApi.Helpers;
using iRentApi.Model.Http.Auth;
using iRentApi.Service.Database.Implement;
using Microsoft.Extensions.Options;

namespace iRentApi.Service.Database.Contract
{
    public abstract class IAuthService : IRentService
    {
        protected IAuthService(IRentContext context, IMapper mapper, IOptions<AppSettings> appSettings) : base(context, mapper, appSettings)
        {
        }

        public abstract Task<AuthenticateResponse?> Login(string email, string password);
        public abstract Task<AuthenticateResponse?> RefreshToken(string token);
        public abstract Task RevokeToken(string refreshToken);
    }
}
