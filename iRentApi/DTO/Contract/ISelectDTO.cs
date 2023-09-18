using iRentApi.Model.Entity.Contract;

namespace iRentApi.DTO.Contract
{
    public interface ISelectDTO<TEntity> : IEntityDTO<TEntity> where TEntity : EntityBase
    {
        long Id { get; set; }
    }
}
