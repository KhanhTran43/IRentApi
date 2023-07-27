using iRentApi.DTO.Contract;
using iRentApi.Model.Entity.Contract;
using iRentApi.Service.Contract;
using iRentApi.Service.ServiceException;
using Microsoft.AspNetCore.Mvc;

namespace iRentApi.Controllers.Contract
{
    public abstract class CrudController<TEntity, TSelect, TInsert, TUpdate> : IRentController, ICrudController<TEntity, TSelect, TInsert, TUpdate>
        where TEntity : EntityBase
        where TSelect : ISelectDTO<TEntity>
        where TInsert : IInsertDTO<TEntity>
        where TUpdate : IUpdateDTO<TEntity>
    {
        public CrudController(IServiceWrapper serviceWrapper) : base(serviceWrapper)
        {
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TSelect>>> GetAll([FromQuery] SelectOptions options)
        {
            try
            {
                return await Service.EntityService<TEntity>().SelectAll<TSelect>(options);
            }
            catch (EntitySetEmptyException)
            {
                return NotFoundResult();
            }
        }
    }
}
