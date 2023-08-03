using AutoMapper;
using Data.Context;
using iRentApi.Helpers;

namespace iRentApi.Service.Contract
{
    public interface IService
    {
        IRentContext Context { get; }
        IMapper Mapper { get; }
        AppSettings AppSettings { get; }
    }
}
