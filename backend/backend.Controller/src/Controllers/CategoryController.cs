using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using backend.Business.src.Abstractions;
using backend.Business.src.Dtos;
using backend.Domain.src.Entities;

namespace backend.Controller.src.Controllers
{
    public class CategoryController : BaseController<Category, CategoryReadDto, CategoryCreateDto, CategoryUpdateDto>
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService baseService) : base(baseService)
        {
            _categoryService = baseService;
        }

        [Authorize(Roles = "Admin")]
        public override async Task<ActionResult<CategoryReadDto>> CreateOne([FromBody] CategoryCreateDto newEntity)
        {
            var createdObject = await _categoryService.CreateOne(newEntity);
            return CreatedAtAction(nameof(CreateOne), createdObject);
        }

        [Authorize(Roles = "Admin")]
        public override async Task<ActionResult<CategoryReadDto>> UpdateOneById([FromRoute] Guid id, [FromBody] CategoryUpdateDto update)
        {
            var updatedObject = await _categoryService.UpdateOneById(id, update);
            return Ok(updatedObject);
        }

        [Authorize(Roles = "Admin")]
        public override async Task<ActionResult<bool>> DeleteOneById ([FromRoute] Guid id)
        {
            return StatusCode(204, await _categoryService.DeleteOneById(id));
        }
    }
}