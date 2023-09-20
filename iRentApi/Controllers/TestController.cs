using iRentApi.Controllers.Contract;
using iRentApi.Model.Entity;
using iRentApi.Service.Database;
using Microsoft.AspNetCore.Mvc;

namespace iRentApi.Controllers
{
    public class TestController : IRentController
    {
        public TestController(IUnitOfWork serviceWrapper) : base(serviceWrapper)
        {
        }

        [HttpPost("emum")]
        public async Task<EnumObject> TestEnum([FromBody]EnumObject enumObject)
        {
            return enumObject;
        }
    }

    public class EnumObject
    {
        public int Name { get; set; }
        public Ward Ward { get; set; }
    }
}
