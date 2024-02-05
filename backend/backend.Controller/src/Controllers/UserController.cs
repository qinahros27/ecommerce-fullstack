using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using backend.Business.src.Abstractions;
using backend.Business.src.Dtos;
using backend.Domain.src.Entities;
using backend.Domain.src.Shared;
using backend.Business.src.Shared; 

namespace backend.Controller.src.Controllers
{
    public class UserController : BaseController<User, UserReadDto, UserCreateDto, UserUpdateDto>
    {
        private readonly IUserService _userService; 
        public UserController(IUserService baseService) : base(baseService)
        {
            _userService = baseService;
        }

        [HttpPost("create-admin")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<UserReadDto>> CreateAdmin([FromBody] UserCreateDto dto)
        {
            return CreatedAtAction(nameof(CreateAdmin), await _userService.CreateAdmin(dto));
        }

        public override async Task<ActionResult<IEnumerable<UserReadDto>>> GetAll([FromQuery] QueryOptions queryOptions)
        {
            return Ok(await _userService.GetAll(queryOptions));
        }

        public override async Task<ActionResult<UserReadDto>> GetOneById([FromRoute] Guid id)
        {
            return Ok(await _userService.GetOneById(id));
        }

        [Authorize]
        public override async Task<ActionResult<UserReadDto>> UpdateOneById([FromRoute] Guid id, [FromBody] UserUpdateDto update)
        {
            var updatedObject = await _userService.UpdateOneById(id, update);
            return Ok(updatedObject);
        }

        [Authorize]
        public override async Task<ActionResult<bool>> DeleteOneById ([FromRoute] Guid id)
        {
            return StatusCode(204, await _userService.DeleteOneById(id));
        }

        [HttpPatch("change-password")]
        [Authorize]
        public async Task<ActionResult<bool>> Changepassword ([FromRoute] Guid id, [FromBody] UserChangePasswordDto updatePassword)
        {
            return StatusCode(204, await _userService.UpdatePassword(id,updatePassword));
        }
    }
}