using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using backend.Business.src.Abstractions;
using backend.Business.src.Dtos;
using backend.Domain.src.Entities;
using backend.Domain.src.Shared;

namespace backend.Controller.src.Controllers
{
    [Authorize]
    public class OrderController : BaseController<Order, OrderReadDto, OrderCreateDto, OrderUpdateDto>
    {
        private readonly IAuthorizationService _authorizationService;
        private readonly IOrderService _orderService;
        
        public OrderController(IOrderService baseService, IAuthorizationService authService) : base(baseService)
        {
            _orderService = baseService;
            _authorizationService = authService;
        }
        
        [Authorize(Roles = "Admin")]
        public override async Task<ActionResult<IEnumerable<OrderReadDto>>> GetAll([FromQuery] QueryOptions queryOptions)
        {
            return Ok(await _orderService.GetAll(queryOptions));
        }

        [Authorize(Roles = "Admin")]
        public override async Task<ActionResult<OrderReadDto>> UpdateOneById([FromRoute] Guid id, [FromBody] OrderUpdateDto update)
        {
            var updatedObject = await _orderService.UpdateOneById(id, update);
            return Ok(updatedObject);
        }
    }
}