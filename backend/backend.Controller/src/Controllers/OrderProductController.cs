using backend.Business.src.Abstractions;
using backend.Business.src.Dtos;
using backend.Domain.src.Entities;

namespace backend.Controller.src.Controllers
{
    public class OrderProductController : BaseController<OrderProduct, OrderProductReadDto, OrderProductCreateDto, OrderProductUpdateDto>
    {
        public OrderProductController(IOrderProductService baseService) : base(baseService)
        {
        }
    }
}