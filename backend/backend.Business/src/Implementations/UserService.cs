using AutoMapper;
using backend.Business.src.Abstractions;
using backend.Business.src.Dtos;
using backend.Domain.src.Entities;
using backend.Domain.src.Abstractions;
using backend.Business.src.Shared;

namespace backend.Business.src.Implementations
{
    public class UserService : BaseService<User, UserReadDto, UserCreateDto, UserUpdateDto>, IUserService
    {
        private readonly IUserRepo _userRepo;
        public UserService(IUserRepo userRepo, IMapper mapper) : base(userRepo, mapper)
        {
            _userRepo = userRepo;
        }

        public async Task<UserReadDto> UpdatePassword(Guid id, UserChangePasswordDto newPassword)
        {
            var foundUser = await _userRepo.GetOneById(id);
            if(foundUser == null)
            {
                throw ServiceException.NotFoundException("User not found");
            }
            PasswordService.HashPassword(newPassword.Password, out var hashedPassword, out var salt);
            foundUser.Password = hashedPassword;
            foundUser.Salt = salt;
            return _mapper.Map<UserReadDto>(await _userRepo.UpdatePassword(foundUser));
        }

        public override async Task<UserReadDto> CreateOne(UserCreateDto dto)
        {
            var entity = _mapper.Map<User>(dto);
            PasswordService.HashPassword(dto.Password, out var hashedPassword, out var salt);
            entity.Password = hashedPassword;
            entity.Salt = salt;
            entity.Role = Role.Customer;
            var created = await _userRepo.CreateOne(entity);
            return _mapper.Map<UserReadDto>(created);
        }

        public async Task<UserReadDto> FindOneByEmail(string email)
        {
            return _mapper.Map<UserReadDto>(await _userRepo.FindOneByEmail(email));
        }

        public async Task<UserReadDto> CreateAdmin(UserCreateDto newAdmin)
        {
            var entity = _mapper.Map<User>(newAdmin);
            PasswordService.HashPassword(newAdmin.Password, out var hashedPassword, out var salt);
            entity.Password = hashedPassword;
            entity.Salt = salt;
            entity.Role = Role.Admin;
            var createdAdmin = await _userRepo.CreateAdmin(entity);
            return _mapper.Map<UserReadDto>(createdAdmin); 
        }
    }
}