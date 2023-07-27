using AutoMapper;
using Data.Context;

namespace iRentApi.Service.Contract
{
    public interface IService
    {
        protected IRentContext Context { get; }
        protected IMapper Mapper { get; }
    }
}
