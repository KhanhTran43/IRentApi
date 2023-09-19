using AutoMapper;
using Data.Context;
using iRentApi.Helpers;

namespace iRentApi.Service.Database.Contract
{
    public abstract class IService
    {
        protected abstract IRentContext Context { get; }
        protected abstract IMapper Mapper { get; }
        protected abstract AppSettings AppSettings { get; }
    }
}
