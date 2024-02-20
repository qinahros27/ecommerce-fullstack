using backend.Business.src.Abstractions;
using backend.Business.src.Dtos;
using backend.Domain.src.Entities;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controller.src.Controllers
{
    public class OrderProductController : BaseController<OrderProduct, OrderProductReadDto, OrderProductCreateDto, OrderProductUpdateDto>
    {
        private readonly IOrderProductService _orderProductService;
        public OrderProductController(IOrderProductService baseService) : base(baseService)
        {
            _orderProductService = baseService;
        }
 
        [HttpGet("orderId/{orderId:Guid}")]
        public async Task<ActionResult<IEnumerable<OrderOfOrderProductReadDto>>> GetAllByOrderId(Guid orderId)
        {
            var orderProducts = await _orderProductService.GetAllByOrderId(orderId);
            return Ok(orderProducts);
        }
    }
}