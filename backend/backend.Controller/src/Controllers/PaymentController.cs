using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using backend.Business.src.Abstractions;
using backend.Business.src.Dtos;
using backend.Domain.src.Entities;
using backend.Domain.src.Shared;

namespace backend.Controller.src.Controllers
{
    public class PaymentController : BaseController<Payment, PaymentReadDto, PaymentCreateDto, PaymentUpdateDto>
    {
        private readonly IPaymentService _paymentService;
  
        public PaymentController(IPaymentService baseService) : base(baseService)
        {
            _paymentService = baseService;
        }
        
        [Authorize(Roles = "Admin")]
        public override async Task<ActionResult<IEnumerable<PaymentReadDto>>> GetAll([FromQuery] QueryOptions queryOptions)
        {
            return Ok(await _paymentService.GetAll(queryOptions));
        }
        
        [Authorize(Roles = "Admin")]
        public override async Task<ActionResult<PaymentReadDto>> GetOneById([FromRoute] Guid id)
        {
            return Ok(await _paymentService.GetOneById(id));
        }
        
        [Authorize]
        public override async Task<ActionResult<PaymentReadDto>> CreateOne([FromBody] PaymentCreateDto newEntity)
        {
            var createdObject = await _paymentService.CreateOne(newEntity);
            return CreatedAtAction(nameof(CreateOne), createdObject);
        }

        [Authorize]
        public override async Task<ActionResult<PaymentReadDto>> UpdateOneById([FromRoute] Guid id, [FromBody] PaymentUpdateDto update)
        {
            
            var updatedObject = await _paymentService.UpdateOneById(id, update);
            return Ok(updatedObject);
        }

        [Authorize]
        public override async Task<ActionResult<bool>> DeleteOneById ([FromRoute] Guid id)
        {
            return StatusCode(204, await _paymentService.DeleteOneById(id));
        }
    }
}