using AutoMapper;
using Data.Context;
using Domain.Model.Entity;
using iRentApi.DTO;
using iRentApi.Helpers;
using iRentApi.Service.Database.Contract;
using Microsoft.Extensions.Options;
using System.Drawing;

namespace iRentApi.Service.Database.Implement
{
    public abstract class IRentService : IService
    {
        private readonly IRentContext _context;
        private readonly IMapper _mapper;
        private readonly AppSettings _appSettings;

        protected override IRentContext Context => _context;
        protected override IMapper Mapper => _mapper;
        protected override AppSettings AppSettings => _appSettings;

        protected IRentService(IRentContext context, IMapper mapper, IOptions<AppSettings> appSettings)
        {
            _context = context;
            _mapper = mapper;
            _appSettings = appSettings.Value;
        }

    }
}
