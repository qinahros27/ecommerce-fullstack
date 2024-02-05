using backend.Business.src.Dtos;
using backend.Domain.src.Entities;

namespace backend.Business.src.Abstractions
{
    public interface IAuthService
    {
        Task<string> VerifyCredentials(UserCredentialsDto credentials);
        Task<UserReadDto> GetUserFromToken(Token token);
    }
}