using backend.Domain.src.Entities;

namespace backend.Domain.src.Abstractions
{
    public interface IOrderProductRepo: IBaseRepo<OrderProduct>
    {
        Task<IEnumerable<OrderProduct>> GetAllByOrderId(Guid orderId);
    }
}