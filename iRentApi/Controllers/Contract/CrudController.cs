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
        public virtual async Task<ActionResult<IEnumerable<TSelect>>> GetAll([FromQuery] SelectOptions options)
        {
            try
            {
                var entities = await Service.EntityService<TEntity>().SelectAll<TSelect>(options);
                return entities;
            }
            catch (EntitySetEmptyException)
            {
                return NotFoundResult();
            }
        }

        [HttpGet("{id}")]
        public virtual async Task<ActionResult<TSelect>> Get([FromRoute] long id,[FromQuery] SelectOptions options)
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
        public virtual async Task<ActionResult<TEntity>> Add(TInsert insert)
        {
            var service = Service.EntityService<TEntity>();

            if (service == null)
            {
                return Problem($"Entity set 'IRentContext.{nameof(TEntity)}s'  is null.");
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

        [HttpDelete("{id}")]
        public virtual async Task<IActionResult> Delete(long id)
        {
            var service = Service.EntityService<TEntity>();

            if (service == null)
            {
                return Problem($"Entity set 'IRentContext.{nameof(TEntity)}s'  is null.");
            }

            try
            {
                await service.Delete(id);
                return Ok("Deleted");
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
        }
    }
}
