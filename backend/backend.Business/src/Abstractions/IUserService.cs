using backend.Business.src.Dtos;
using backend.Domain.src.Entities;

namespace backend.Business.src.Abstractions
{
    public interface IUserService : IBaseService<User, UserReadDto, UserCreateDto, UserUpdateDto>
    {
        Task<UserReadDto> UpdatePassword(Guid id, UserChangePasswordDto newPassword);
        Task<UserReadDto> CreateAdmin(UserCreateDto newAdmin);
        Task<UserReadDto> FindOneByEmail(string email);
    }
}