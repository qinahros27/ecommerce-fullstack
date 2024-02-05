using AutoMapper;
using Moq;

using backend.Domain.src.Shared;
using backend.Business.src.Abstractions;
using backend.Business.src.Dtos;
using backend.Business.src.Implementations;
using backend.Business.src.Shared;
using backend.Domain.src.Abstractions;
using backend.Domain.src.Entities;
using backend.Infrastructure.src.Configuration;

namespace backend.Testing.src.Business.Tests
{
    public class UserBusinessTest
    {
        private Mock<IUserRepo> _mockUserRepo;
        private IUserService _userService;
        private IMapper _mapper;

        public UserBusinessTest() 
        {
            _mockUserRepo = new();
            _mapper = new MapperConfiguration(cfg => cfg.AddProfile<MapperProfile>()).CreateMapper();
            _userService = new UserService(_mockUserRepo.Object, _mapper);
        }

        [Fact]
        public async void CreateNewUser_ValidData_ReturnNewUser()
        {
            //arrange
            _mockUserRepo.Setup(x => x.CreateOne(It.IsAny<User>())).ReturnsAsync((User newUser) => new User());
            

            //act
            var newUser = new UserCreateDto {
                FirstName = "Anh",
                LastName = "Nguyen",
                Address = "Red road",
                PostCode = "50100",
                City = "Mikkeli",
                Country = "Finland",
                Email = "anhnguyen@mail.com",
                Avatar = "avatar1",
                Password = "anh123",
                UserName = "anh"
            };
            var createdUser = await _userService.CreateOne(newUser);

            //assert
            Assert.NotNull(createdUser); 
            _mockUserRepo.Verify(x => x.CreateOne(It.IsAny<User>()), Times.Once());
        }
        
        [Fact]
        public async Task LoginUser_ValidCredentials_ReturnsToken()
        {
            var authService = new AuthService(_mockUserRepo.Object,_mapper);
            // Arrange
            var credentials = new UserCredentialsDto
            {
                Email = "test@example.com",
                Password = "password123"
            };

            var foundUser = new User
            {
                Id = Guid.NewGuid(),
                Email = "test@example.com",
                Password = "hashedPassword",
                Salt = new byte[10],
                Role = Role.Customer
            };

            _mockUserRepo.Setup(repo => repo.FindOneByEmail(credentials.Email)).ReturnsAsync(foundUser);

            var mockPasswordService = new Mock<IPasswordService>();
            mockPasswordService.Setup(service => service.VerifyPassword(credentials.Password, foundUser.Password, foundUser.Salt)).Returns(true);
            
            // Act
            var token = await authService.VerifyCredentials(credentials);

            // Assert
            Assert.NotNull(token);
            Assert.NotEmpty(token);
        }

        [Fact]
        public void LoginUser_WrongCredentials_Error()
        {
            var authService = new AuthService(_mockUserRepo.Object,_mapper);
            var loginDto = new UserCredentialsDto
            {
                Email = "error@mail.com",
                Password = "error"
            };

            Assert.ThrowsAsync<Exception>(() => authService.VerifyCredentials(loginDto));
        }

        [Fact]
        public async void GetUser_ValidData_ReturnUser()
        {
            var expectedId = Guid.NewGuid();
            var expectedFirstName = "Anh";
        
            var User = new User {
                FirstName = "Anh",
                LastName = "Nguyen",
                Email = "anhnguyen@mail.com",
                Avatar = "avatar"
            };
            //arrange
            _mockUserRepo.Setup(x => x.GetOneById(expectedId)).ReturnsAsync(User);
            
            //act
            var result = await _userService.GetOneById(expectedId);

            //assert
            Assert.NotNull(result);
            Assert.Equal(expectedFirstName, result.FirstName);
            _mockUserRepo.Verify(x => x.GetOneById(expectedId), Times.Once());
        }

        [Fact]
        public async void GetAllUser_ValidData_ReturnListUsers()
        {
            //arrange
            var UserList = new List<User>
            {
                new User { Id = Guid.NewGuid(), FirstName = "Anh", LastName = "Nguyen", Email = "anh@mail.com", Password = "anh123" , Avatar = "avatar1", Address = "address1"},
                new User { Id = Guid.NewGuid(), FirstName = "Anh2", LastName = "Nguyen2", Email = "anh2@mail.com", Password = "anh123" , Avatar = "avatar2", Address = "address2" },
                new User { Id = Guid.NewGuid(), FirstName = "Anh3", LastName = "Nguyen3", Email = "anh3@mail.com", Password = "anh123" , Avatar = "avatar3", Address = "address3" },
                new User { Id = Guid.NewGuid(), FirstName = "Anh4", LastName = "Nguyen4", Email = "anh4@mail.com", Password = "anh123" , Avatar = "avatar4", Address = "address4" }
            };

            var queryOptions = new QueryOptions
            {
                Search = string.Empty,
                Order = string.Empty,
                Offset = 0,
                Limit = 4,
                OrderByDescending = false
            };
            _mockUserRepo.Setup(x => x.GetAll(queryOptions)).ReturnsAsync(UserList);
            
            //act
            var result = await _userService.GetAll(queryOptions);

            //assert
            Assert.NotNull(result);
            Assert.Equal(UserList.Count, result.Count());
            _mockUserRepo.Verify(x => x.GetAll(queryOptions), Times.Once());
        }

        [Fact]
        public async void UpdateUser_ValidData_ReturnUser()
        {
            //arrange
            var userId = Guid.NewGuid();
            var updatedDto = new UserUpdateDto { FirstName = "AnhUpdated" };

            var foundUser = new User { Id = userId, FirstName = "Anh", LastName = "Nguyen", Email = "anh@mail.com", Password = "anh123" , Avatar = "avatar1", Address = "address1" };

            _mockUserRepo.Setup(x => x.GetOneById(userId)).ReturnsAsync(foundUser);
            _mockUserRepo.Setup(x => x.UpdateOneById(foundUser)).ReturnsAsync(foundUser);

            var mockMapper = new Mock<IMapper>();
            mockMapper.Setup(mapper => mapper.Map(updatedDto, foundUser))
                .Callback<UserUpdateDto, User>((dto, entity) =>
                {
                    entity.FirstName = dto.FirstName;
                    entity.LastName = dto.LastName;
                    entity.Avatar = dto.Avatar;
                });

            mockMapper.Setup(mapper => mapper.Map<UserReadDto>(foundUser)).Returns(new UserReadDto { FirstName = "AnhUpdated" });

            
            //act
             var result = await _userService.UpdateOneById(userId, updatedDto);
      
            //assert
            Assert.NotNull(result);
            Assert.Equal(updatedDto.FirstName, result.FirstName);
            _mockUserRepo.Verify(x => x.UpdateOneById(foundUser), Times.Once());
        }

        [Fact]
        public async void UpdateUser_InValidData_ReturnErrorMessage()
        {
            //arrange
            var userId = Guid.NewGuid();
            var updatedDto = new UserUpdateDto { FirstName = "AnhUpdated" };

            var foundUser = new User { Id = userId, FirstName = "Anh", LastName = "Nguyen", Email = "anh@mail.com", Password = "anh123" , Avatar = "avatar1", Address = "address1" };
            
            _mockUserRepo.Setup(x => x.GetOneById(userId)).ReturnsAsync(foundUser);
            _mockUserRepo.Setup(x => x.UpdateOneById(foundUser)).ReturnsAsync(foundUser);

            var mockMapper = new Mock<IMapper>();
            mockMapper.Setup(mapper => mapper.Map(updatedDto, foundUser))
                .Callback<UserUpdateDto, User>((dto, entity) =>
                {
                    entity.FirstName = dto.FirstName;
                    entity.LastName = dto.LastName;
                    entity.Avatar = dto.Avatar;
                });

            mockMapper.Setup(mapper => mapper.Map<UserReadDto>(foundUser)).Returns(new UserReadDto { FirstName = "AnhUpdated" });

            var anotherId = Guid.NewGuid();
      
            //assert
            Assert.ThrowsAsync<Exception>(async () => await _userService.UpdateOneById(anotherId, updatedDto));
        }

        [Fact]
        public async Task DeleteOneById_UserFound_ReturnsTrue()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var foundUser = new User { Id = userId, FirstName = "Anh", LastName = "Nguyen", Email = "anh@mail.com", Password = "anh123" , Avatar = "avatar1", Address = "address1" };

            _mockUserRepo.Setup(repo => repo.GetOneById(userId)).ReturnsAsync(foundUser);
            _mockUserRepo.Setup(repo => repo.DeleteOneById(foundUser)).Returns(Task.FromResult(true));

            // Act
            var result = await _userService.DeleteOneById(userId);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task DeleteOneById_UserNotFound_ReturnsFalse()
        {
            // Arrange
            var userId = Guid.NewGuid();
            _mockUserRepo.Setup(repo => repo.GetOneById(userId)).ReturnsAsync((User)null);

            // Act
            var result = await _userService.DeleteOneById(userId);

            // Assert
            Assert.False(result);
        }
    }
}