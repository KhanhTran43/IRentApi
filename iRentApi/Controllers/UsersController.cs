using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Data.Context;
using Domain.Model.Entity;

namespace iRentApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : EntityController<User, long>
    {
        public UserController(IRentContext context) : base(context)
        {
        }
    }
}
