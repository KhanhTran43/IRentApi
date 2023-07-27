using Domain.Model.Entity;
using iRentApi.DTO;
using iRentApi.DTO.Contract;

namespace iRentApi.Service.Contract
{
    public interface IUserService : IEntityCRUDService<User>
    {
    }
}
