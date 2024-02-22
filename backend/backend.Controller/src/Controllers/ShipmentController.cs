using backend.Business.src.Abstractions;
using backend.Business.src.Dtos;
using backend.Domain.src.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controller.src.Controllers
{
    public class ShipmentController : BaseController<Shipment, ShipmentReadDto, ShipmentCreateDto, ShipmentUpdateDto>
    {
        private readonly IShipmentService _shipmentService;
        public ShipmentController(IShipmentService baseService) : base(baseService)
        {
            _shipmentService = baseService;
        }

        [Authorize(Roles = "Admin")]
        public override async Task<ActionResult<ShipmentReadDto>> CreateOne([FromBody] ShipmentCreateDto newEntity)
        {
            var createdObject = await _shipmentService.CreateOne(newEntity);
            return CreatedAtAction(nameof(CreateOne), createdObject);
        }

        [Authorize(Roles = "Admin")]
        public override async Task<ActionResult<ShipmentReadDto>> UpdateOneById([FromRoute] Guid id, [FromBody] ShipmentUpdateDto update)
        {
            var updatedObject = await _shipmentService.UpdateOneById(id, update);
            return Ok(updatedObject);
        }

        [Authorize(Roles = "Admin")]
        public override async Task<ActionResult<bool>> DeleteOneById ([FromRoute] Guid id)
        {
            return StatusCode(204, await _shipmentService.DeleteOneById(id));
        }

        [HttpGet("orderProduct/{orderProductId:Guid}")]
        public async Task<ActionResult<OrderProductShipmentReadDto>> GetOneByOrderProductId(Guid orderProductId)
        {
            return Ok(await _shipmentService.GetOneByOrderProductId(orderProductId));
        }
    }
}