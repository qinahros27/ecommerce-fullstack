using AutoMapper;
using backend.Business.src.Abstractions;
using backend.Business.src.Dtos;
using backend.Domain.src.Entities;
using backend.Domain.src.Abstractions;

namespace backend.Business.src.Implementations
{
    public class ShipmentService : BaseService<Shipment, ShipmentReadDto, ShipmentCreateDto, ShipmentUpdateDto>, IShipmentService
    {
        public ShipmentService(IShipmentRepo shipmentRepo, IMapper mapper) : base(shipmentRepo, mapper)
        {
        }
    }
}