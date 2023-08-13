using iRentApi.DTO.Contract;
using iRentApi.Model.Entity.Contract;
using iRentApi.Service.Contract;
using iRentApi.Service.ServiceException;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

        [HttpGet("{id}")]
        public async Task<ActionResult<TSelect>> Get([FromRoute] long id,[FromQuery] SelectOptions options)
        {
            try
            {
                return await Service.EntityService<TEntity>().SelectByID<TSelect>(id, options);
            }
            catch (EntitySetEmptyException)
            {
                return NotFoundResult();
            }
        }

        [HttpPost]
        public async Task<ActionResult<TEntity>> Add(TInsert insert)
        {
            var service = Service.EntityService<TEntity>();

            if (service == null)
            {
                return Problem("Entity set 'IRentContext.Users'  is null.");
            }

            try
            {
                var entity = await service.Insert(insert);
                return CreatedAtAction("Get", new { id = entity.Id }, entity);
            }
            catch(Exception e)
            {
                return Problem(e.Message);
            }
        }
    }
}
