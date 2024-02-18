using backend.Business.src.Abstractions;
using backend.Business.src.Dtos;
using backend.Domain.src.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace backend.Controller.src.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]s")]
    public class UserCardController : ControllerBase
    {
        private readonly IUserCardService _userCardService;
        private readonly IAuthorizationService _authorizationService;
        public UserCardController(IUserCardService baseService , IAuthorizationService authService) 
        {
            _userCardService = baseService;
            _authorizationService = authService;
        }

        [HttpGet("{id:Guid}")]
        [Authorize]
        public virtual async Task<ActionResult<UserCardReadDto>> GetOneById([FromRoute] Guid id)
        {
            var user = HttpContext.User;
            var card = await _userCardService.GetOneById(id);
            
            var authorizeOwner = await _authorizationService.AuthorizeAsync(user, card, "OwnerOnly");
            if(authorizeOwner.Succeeded)
            {
                return Ok(await _userCardService.GetOneById(id));
            }
            else
            {
                return new ForbidResult();
            }
        }

        [HttpPost]
        [Authorize]
        public virtual async Task<ActionResult<UserCardReadDto>> CreateOne([FromBody] UserCardCreateDto newEntity)
        {
            var createdObject = await _userCardService.CreateOne(newEntity);
            return CreatedAtAction(nameof(CreateOne), createdObject);
        }

        [HttpPatch("{id:Guid}")]
        [Authorize]
        public virtual async Task<ActionResult<UserCardReadDto>> UpdateOneById([FromRoute] Guid id, [FromBody] UserCardUpdateDto update)
        {
            var user = HttpContext.User;
            var card = await _userCardService.GetOneById(id);
            
            var authorizeOwner = await _authorizationService.AuthorizeAsync(user, card, "OwnerOnly");
            if(authorizeOwner.Succeeded)
            {
                var updatedObject = await _userCardService.UpdateOneById(id, update);
                return Ok(updatedObject);
            }
            else
            {
                return new ForbidResult();
            }
        }

        [HttpDelete("{id:Guid}")]
        [Authorize]
        public virtual async Task<ActionResult<bool>> DeleteOneById ([FromRoute] Guid id)
        {
            var user = HttpContext.User;
            var card = await _userCardService.GetOneById(id);
            
            var authorizeOwner = await _authorizationService.AuthorizeAsync(user, card, "OwnerOnly");
            if(authorizeOwner.Succeeded)
            {
                return StatusCode(204, await _userCardService.DeleteOneById(id));
            }
            else
            {
                return new ForbidResult();
            }
        }
    }
}