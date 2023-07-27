using iRentApi.DTO;
using iRentApi.DTO.Contract;
using iRentApi.Model.Entity.Contract;
using iRentApi.Service.Contract;
using iRentApi.Service.ServiceException;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace iRentApi.Controllers.Contract
{
    public interface ICrudController<TEntity, TSelect, TInsert, TUpdate> : IController
        where TEntity : EntityBase
        where TSelect : ISelectDTO<TEntity>
        where TInsert : IInsertDTO<TEntity>
        where TUpdate : IUpdateDTO<TEntity>
    {
        public Task<ActionResult<IEnumerable<TSelect>>> GetAll(SelectOptions options);
    }
}
