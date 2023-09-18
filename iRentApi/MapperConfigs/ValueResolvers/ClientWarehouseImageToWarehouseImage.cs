using AutoMapper;
using iRentApi.DTO;
using iRentApi.Model.Entity;
using Stripe;
using System.Text.Json;

namespace iRentApi.MapperConfigs.TypeConverters
{
    public class ClientWarehouseImageToWarehouseImage : IValueResolver<ClientWarehouseImage, WarehouseImage, string>
    {
        public string Resolve(ClientWarehouseImage source, WarehouseImage destination, string destMember, ResolutionContext context)
        {
            return JsonSerializer.Serialize(source);
        }
    }
}
