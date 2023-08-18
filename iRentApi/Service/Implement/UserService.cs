using AutoMapper;
using Data.Context;
using Domain.Model.Entity;
using iRentApi.DTO;
using iRentApi.Helpers;
using iRentApi.Service.Contract;
using Microsoft.Extensions.Options;

namespace iRentApi.Service.Implement
{
    public class UserService : IUserService
    {
        public UserService(IRentContext context, IMapper mapper, IOptions<AppSettings> appSettings) : base(context, mapper, appSettings)
        {
        }
    }
}
