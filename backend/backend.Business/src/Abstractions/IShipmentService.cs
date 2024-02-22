using backend.Domain.src.Entities;
using backend.Business.src.Dtos;

namespace backend.Business.src.Abstractions
{
    public interface IShipmentService : IBaseService<Shipment, ShipmentReadDto, ShipmentCreateDto, ShipmentUpdateDto>
    {
        
        Task<OrderProductShipmentReadDto> GetOneByOrderProductId(Guid orderProductId);
    }
}