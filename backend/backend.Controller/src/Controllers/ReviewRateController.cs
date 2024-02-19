using backend.Business.src.Abstractions;
using backend.Business.src.Dtos;
using backend.Domain.src.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace backend.Controller.src.Controllers
{
    public class ReviewRateController : BaseController<ReviewRate, ReviewRateReadDto, ReviewRateCreateDto, ReviewRateUpdateDto>
    {
        private readonly IReviewRateService _reviewRateService;
        public ReviewRateController(IReviewRateService baseService) : base(baseService)
        {
            _reviewRateService = baseService;
        }

        [Authorize]
        public override async Task<ActionResult<ReviewRateReadDto>> CreateOne([FromBody] ReviewRateCreateDto newEntity)
        {
            var createdObject = await _reviewRateService.CreateOne(newEntity);
            return CreatedAtAction(nameof(CreateOne), createdObject);
        }

        [Authorize]
        public override async Task<ActionResult<ReviewRateReadDto>> UpdateOneById([FromRoute] Guid id, [FromBody] ReviewRateUpdateDto update)
        {
            var updatedObject = await _reviewRateService.UpdateOneById(id, update);
            return Ok(updatedObject);
        }

        [Authorize]
        public override async Task<ActionResult<bool>> DeleteOneById ([FromRoute] Guid id)
        {
            return StatusCode(204, await _reviewRateService.DeleteOneById(id));
        }
        
        [HttpGet("productId/{productId:Guid}")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<ProductReviewRateReadDto>>> GetAllByProduct([FromRoute] Guid productId)
        {
            var userReviews = await _reviewRateService.GetAllByProduct(productId);
            return Ok(userReviews);
        }

        [HttpGet("userId/{userId:Guid}")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<UserReviewRateReadDto>>> GetAllByUser([FromRoute] Guid userId)
        {
            var productReviews = await _reviewRateService.GetAllByUser(userId);
            return Ok(productReviews);
        }
    }
}