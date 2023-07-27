using AutoMapper;
using Data.Context;
using Domain.Model.Entity;
using iRentApi.DTO;
using iRentApi.Service.Contract;
using System.Drawing;

namespace iRentApi.Service.Implement
{
    public abstract class IRentService : IService
    {
        private readonly IRentContext _context;
        private readonly IMapper _mapper;

        IRentContext IService.Context => _context;

        IMapper IService.Mapper => _mapper;

        protected IRentService(IRentContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

    }
}
