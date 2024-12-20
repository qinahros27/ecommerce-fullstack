using AutoMapper;
using backend.Business.src.Abstractions;
using backend.Business.src.Dtos;
using backend.Domain.src.Entities;
using backend.Domain.src.Abstractions;

namespace backend.Business.src.Implementations
{
    public class UserCardService : BaseService<UserCard, UserCardReadDto, UserCardCreateDto, UserCardUpdateDto>, IUserCardService
    {
        public UserCardService(IUserCardRepo userCardRepo, IMapper mapper) : base(userCardRepo, mapper)
        {
        }
    }
}