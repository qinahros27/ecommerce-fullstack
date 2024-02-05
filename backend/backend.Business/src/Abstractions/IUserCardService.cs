using backend.Domain.src.Entities;
using backend.Business.src.Dtos;

namespace backend.Business.src.Abstractions
{
    public interface IUserCardService : IBaseService<UserCard, UserCardReadDto, UserCardCreateDto, UserCardUpdateDto>
    {
    }
}