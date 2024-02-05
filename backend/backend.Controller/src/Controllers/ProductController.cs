using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using backend.Business.src.Abstractions;
using backend.Business.src.Dtos;
using backend.Domain.src.Shared;

namespace backend.Controller.src.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]s")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductController(IProductService baseService) 
        {
            _productService = baseService;
        }

        [HttpGet]
        public virtual async Task<ActionResult<IEnumerable<ProductReadDto>>> GetAll([FromQuery] QueryOptionProduct queryOptions)
        {
            return Ok(await _productService.GetAll(queryOptions));
        }

        [HttpGet("{id:Guid}")]
        public virtual async Task<ActionResult<ProductReadDto>> GetOneById([FromRoute] Guid id)
        {
            return Ok(await _productService.GetOneById(id));
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ProductReadDto>> CreateOne([FromBody] ProductCreateDto newEntity)
        {
            var createdObject = await _productService.CreateOne(newEntity);
            return CreatedAtAction(nameof(CreateOne), createdObject);
        }

        [HttpPatch("{id:Guid}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ProductReadDto>> UpdateOneById([FromRoute] Guid id, [FromBody] ProductUpdateDto update)
        {
            var updatedObject = await _productService.UpdateOneById(id, update);
            return Ok(updatedObject);
        }

        [HttpDelete("{id:Guid}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<bool>> DeleteOneById ([FromRoute] Guid id)
        {
            return StatusCode(204, await _productService.DeleteOneById(id));
        }
    }
}