using iRentApi.DTO.Contract;
using iRentApi.Model.Entity.Contract;
using iRentApi.Model.Service.Crud;
using iRentApi.Service.Database;
using iRentApi.Service.Database.Exception;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace iRentApi.Controllers.Contract
{
    public abstract class CrudController<TEntity, TSelect, TInsert, TUpdate> : IRentController
        where TEntity : EntityBase
        where TSelect : ISelectDTO<TEntity>
        where TInsert : IInsertDTO<TEntity>
        where TUpdate : IUpdateDTO<TEntity>
    {
        public CrudController(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        [HttpPost("static")]
        public virtual async Task<ActionResult<IEnumerable<TSelect>>> GetAllStatic([FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Allow)] GetStaticRequest? request)
        {
            try
            {
                var entities = await Service.EntityService<TEntity>().SelectAll<TSelect>(request);
                return entities;
            }
            catch (EntitySetEmptyException)
            {
                return NotFoundResult();
            }
        }

        [HttpPost("static/{id}")]
        public virtual async Task<ActionResult<TSelect>> GetStatic([FromRoute] long id, [FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Allow)] GetStaticRequest? request)
        {
            try
            {
                return await Service.EntityService<TEntity>().SelectByID<TSelect>(id, request);
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
                var entityEntry = service.Insert(insert);
                var entity = service.MapTo<TSelect>(entityEntry.Entity);
                await service.SaveAsync();
                return CreatedAtAction("GetStatic", new { id = entity.Id }, entity);
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
                service.Delete(id);
                await service.SaveAsync();
                return Ok("Deleted");
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
        }
    }
}
