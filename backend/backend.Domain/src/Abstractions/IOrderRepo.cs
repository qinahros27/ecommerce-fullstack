using backend.Domain.src.Entities;

namespace backend.Domain.src.Abstractions
{
    public interface IOrderRepo: IBaseRepo<Order>
    {
        Task<IEnumerable<Order>> GetAllByUserId(Guid userId);
    }
}