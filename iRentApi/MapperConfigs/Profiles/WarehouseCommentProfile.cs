using AutoMapper;
using iRentApi.DTO;
using iRentApi.Model.Entity;

namespace iRentApi.MapperConfigs.Profiles
{
    public class WarehouseCommentProfile : Profile
    {
        public WarehouseCommentProfile()
        {
            CreateMap<WarehouseComment, WarehouseCommentDTO>();
            CreateMap<CreateWarehouseCommentDTO, WarehouseComment>();
        }
    }
}
