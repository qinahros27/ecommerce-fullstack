using backend.Domain.src.Entities;
using backend.Business.src.Dtos;

namespace backend.Business.src.Abstractions
{
    public interface IOrderProductService : IBaseService<OrderProduct, OrderProductReadDto, OrderProductCreateDto, OrderProductUpdateDto>
    {
        Task<IEnumerable<OrderOfOrderProductReadDto>> GetAllByOrderId(Guid orderId);
    }
}