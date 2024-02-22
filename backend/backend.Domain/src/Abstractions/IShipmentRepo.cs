using backend.Domain.src.Entities;

namespace backend.Domain.src.Abstractions
{
    public interface IShipmentRepo: IBaseRepo<Shipment>
    {
        Task<Shipment> GetOneByOrderProductId(Guid orderProductId);
    }
}