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
    public class OrderBusinessTest
    {
        private Mock<IOrderRepo> _mockOrderRepo;
        private IOrderService _orderService;
        private IMapper _mapper;

        public OrderBusinessTest() 
        {
            _mockOrderRepo = new();
            _mapper = new MapperConfiguration(cfg => cfg.AddProfile<MapperProfile>()).CreateMapper();
            _orderService = new OrderService(_mockOrderRepo.Object, _mapper);
        }

        [Fact]
        public async void CreateNewOrder_ValidData_ReturnNewOrder()
        {
            //arrange
            _mockOrderRepo.Setup(x => x.CreateOne(It.IsAny<Order>())).ReturnsAsync((Order newOrder) => new Order());
            
            //act
            var newOrder = new OrderCreateDto {
                Address = "address",
                PostCode = "postcode",
                City = "city",
                Country = "country",
                PhoneNumber = "0123456789",
                UserId = Guid.NewGuid()
            };
            var createdOrder = await _orderService.CreateOne(newOrder);

            //assert
            Assert.NotNull(createdOrder); 
            _mockOrderRepo.Verify(x => x.CreateOne(It.IsAny<Order>()), Times.Once());
        }

        [Fact]
        public async void GetOrder_ValidData_ReturnOrder()
        {
            var expectedId = Guid.NewGuid();
            var expectedAddress = "address";
        
            var Order = new Order {
                Address = "address",
                PostCode = "postcode",
                City = "city",
                Country = "country",
                PhoneNumber = "0123456789",
                UserId = Guid.NewGuid()
            };
            //arrange
            _mockOrderRepo.Setup(x => x.GetOneById(expectedId)).ReturnsAsync(Order);
            
            //act
            var result = await _orderService.GetOneById(expectedId);

            //assert
            Assert.NotNull(result);
            Assert.Equal(expectedAddress, result.Address);
            _mockOrderRepo.Verify(x => x.GetOneById(expectedId), Times.Once());
        }

        [Fact]
        public async void GetAllOrder_ValidData_ReturnListOrder()
        {
            //arrange
            var OrderList = new List<Order>
            {
                new Order { Id = Guid.NewGuid(), Address = "address", PostCode = "postcode", City = "city", Country = "country", PhoneNumber = "0123456789", UserId = Guid.NewGuid()},
                new Order { Id = Guid.NewGuid(), Address = "address1", PostCode = "postcode1", City = "city1", Country = "country1", PhoneNumber = "0123456781", UserId = Guid.NewGuid()},
                new Order { Id = Guid.NewGuid(), Address = "address2", PostCode = "postcode2", City = "city2", Country = "country2", PhoneNumber = "0123456782", UserId = Guid.NewGuid()},
                new Order { Id = Guid.NewGuid(), Address = "address3", PostCode = "postcode3", City = "city3", Country = "country3", PhoneNumber = "0123456783", UserId = Guid.NewGuid()}
            };

            var queryOptions = new QueryOptions
            {
                Search = string.Empty,
                Order = string.Empty,
                Offset = 0,
                Limit = 4,
                OrderByDescending = false
            };
            _mockOrderRepo.Setup(x => x.GetAll(queryOptions)).ReturnsAsync(OrderList);
            
            //act
            var result = await _orderService.GetAll(queryOptions);

            //assert
            Assert.NotNull(result);
            Assert.Equal(OrderList.Count, result.Count());
            _mockOrderRepo.Verify(x => x.GetAll(queryOptions), Times.Once());
        }

        [Fact]
        public async void UpdateOrder_ValidData_ReturnOrder()
        {
            //arrange
            var orderId = Guid.NewGuid();
            var updatedDto = new OrderUpdateDto { Address = "AddressUpdated" };

            var foundOrder = new Order { Id = orderId, Address = "address", PostCode = "postcode", City = "city", Country = "country", PhoneNumber = "0123456789", UserId = Guid.NewGuid()};

            _mockOrderRepo.Setup(x => x.GetOneById(orderId)).ReturnsAsync(foundOrder);
            _mockOrderRepo.Setup(x => x.UpdateOneById(foundOrder)).ReturnsAsync(foundOrder);

            var mockMapper = new Mock<IMapper>();
            mockMapper.Setup(mapper => mapper.Map(updatedDto, foundOrder))
                .Callback<OrderUpdateDto, Order>((dto, entity) =>
                {
                    entity.Address = dto.Address;
                    entity.PostCode= dto.PostCode;
                    entity.City = dto.City;
                    entity.Country = dto.Country;
                    entity.PhoneNumber = dto.PhoneNumber;
                });

            mockMapper.Setup(mapper => mapper.Map<OrderReadDto>(foundOrder)).Returns(new OrderReadDto { Address = "AddressUpdated" });

            
            //act
             var result = await _orderService.UpdateOneById(orderId, updatedDto);
      
            //assert
            Assert.NotNull(result);
            Assert.Equal(updatedDto.Address, result.Address);
            _mockOrderRepo.Verify(x => x.UpdateOneById(foundOrder), Times.Once());
        } 

        [Fact]
        public async Task DeleteOneById_OrderFound_ReturnsTrue()
        {
            // Arrange
            var orderId = Guid.NewGuid();
            var foundOrder = new Order { Id = orderId, Address = "address", PostCode = "postcode", City = "city", Country = "country", PhoneNumber = "0123456789", UserId = Guid.NewGuid()};

            _mockOrderRepo.Setup(repo => repo.GetOneById(orderId)).ReturnsAsync(foundOrder);
            _mockOrderRepo.Setup(repo => repo.DeleteOneById(foundOrder)).Returns(Task.FromResult(true));

            // Act
            var result = await _orderService.DeleteOneById(orderId);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task DeleteOneById_OrderNotFound_ReturnsFalse()
        {
            // Arrange
            var orderId = Guid.NewGuid();
            _mockOrderRepo.Setup(repo => repo.GetOneById(orderId)).ReturnsAsync((Order)null);

            // Act
            var result = await _orderService.DeleteOneById(orderId);

            // Assert
            Assert.False(result);
        }
    }
}