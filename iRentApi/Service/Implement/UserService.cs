using AutoMapper;
using Data.Context;
using Domain.Model.Entity;
using iRentApi.DTO;
using iRentApi.Service.Contract;

namespace iRentApi.Service.Implement
{
    public class UserService : IRentService, IUserService
    {
        public UserService(IRentContext context, IMapper mapper) : base(context, mapper)
        {
        }
    }
}
