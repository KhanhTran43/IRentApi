using iRentApi.Model.Entity.Contract;

namespace iRentApi.DTO.Contract
{
    public interface IInsertDTO<TEntity> : IEntityDTO<TEntity> where TEntity : EntityBase
    {
    }
}
