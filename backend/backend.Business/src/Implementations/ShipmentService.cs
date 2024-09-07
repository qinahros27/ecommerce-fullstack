using AutoMapper;
using backend.Business.src.Abstractions;
using backend.Business.src.Dtos;
using backend.Domain.src.Entities;
using backend.Domain.src.Abstractions;
using backend.Business.src.Shared;

namespace backend.Business.src.Implementations
{
    public class ShipmentService : BaseService<Shipment, ShipmentReadDto, ShipmentCreateDto, ShipmentUpdateDto>, IShipmentService
    {
        private readonly IShipmentRepo _shipmentRepo;
        public ShipmentService(IShipmentRepo shipmentRepo, IMapper mapper) : base(shipmentRepo, mapper)
        {
            _shipmentRepo = shipmentRepo;
        }

        public async Task<OrderProductShipmentReadDto> GetOneByOrderProductId(Guid orderProductId)
        {
            var foundShipment = await _shipmentRepo.GetOneByOrderProductId(orderProductId);
            if(foundShipment == null)
            {
                throw CustomException.NotFoundException();
            }
            else
            {
                return _mapper.Map<OrderProductShipmentReadDto>(foundShipment);
            }
        }
    }
}