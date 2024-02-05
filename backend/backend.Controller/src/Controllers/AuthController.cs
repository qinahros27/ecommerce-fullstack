using Microsoft.AspNetCore.Mvc;
using backend.Business.src.Abstractions;
using backend.Business.src.Dtos;
using backend.Domain.src.Entities;

namespace backend.Controller.src.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        public async Task<ActionResult<string>> VerifyCredentials([FromBody] UserCredentialsDto credentials)
        {
            return Ok(await _authService.VerifyCredentials(credentials));
        }

        [HttpPost("profile")]
        public async Task<ActionResult<UserReadDto>> GetUserFromToken([FromBody] Token token)
        {
            return Ok(await _authService.GetUserFromToken(token));
        }
    }
}