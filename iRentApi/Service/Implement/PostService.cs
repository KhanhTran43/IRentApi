﻿using AutoMapper;
using Data.Context;
using iRentApi.Helpers;
using iRentApi.Service.Contract;
using Microsoft.Extensions.Options;

namespace iRentApi.Service.Implement
{
    public class PostService : IPostService
    {
        public PostService(IRentContext context, IMapper mapper, IOptions<AppSettings> appSettings) : base(context, mapper, appSettings)
        {
        }
    }
}
