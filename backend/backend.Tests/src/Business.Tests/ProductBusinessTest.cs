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
    public class ProductBusinessTest
    {
        private Mock<IProductRepo> _mockProductRepo;
        private Mock<ICategoryRepo> _mockCategoryRepo;
        private IProductService _productService;
        private IMapper _mapper;

        public ProductBusinessTest() 
        {
            _mockProductRepo = new Mock<IProductRepo>();
            _mockCategoryRepo = new Mock<ICategoryRepo>();
            _mapper = new MapperConfiguration(cfg => cfg.AddProfile<MapperProfile>()).CreateMapper();
            _productService = new ProductService(_mockProductRepo.Object,_mockCategoryRepo.Object, _mapper);
        }

        [Fact]
        public async void CreateNewItem_ValidData_ReturnNewItem()
        {
            // Arrange
            var productCreateDto = new ProductCreateDto
            {
                Title = "Book3",
                Description = "This is a book description.",
                Price = 20,
                Images = new List<string> { "img1.jpg", "img2.jpg", "img3.jpg" },
                Inventory = 50,
                CategoryId = Guid.NewGuid()
            };

            var createdProduct = new Product
            {
                Id = Guid.NewGuid(),
                Title = "Book3",
                Description = "This is a book description.",
                Price = 20,
                Images = new List<string> { "img1.jpg", "img2.jpg", "img3.jpg" },
                Inventory = 50,
                CategoryId = productCreateDto.CategoryId
            };

            _mockProductRepo.Setup(x => x.CreateOne(It.IsAny<Product>())).ReturnsAsync(createdProduct);
            _mockCategoryRepo.Setup(x => x.GetOneById(It.IsAny<Guid>())).ReturnsAsync(new Category());

            // Act
            var result = await _productService.CreateOne(productCreateDto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(createdProduct.Id, result.Id);
            Assert.Equal(createdProduct.Title, result.Title);
            Assert.Equal(createdProduct.Description, result.Description);
            Assert.Equal(createdProduct.Price, result.Price);
            _mockProductRepo.Verify(x => x.CreateOne(It.IsAny<Product>()), Times.Once());
            _mockCategoryRepo.Verify(x => x.GetOneById(It.IsAny<Guid>()), Times.Once());
        }

        [Fact]
        public async void GetItem_ValidData_ReturnItem()
        {
            var expectedId = Guid.NewGuid();
            var expectedTitle = "Book3";
            
            var categoryId = Guid.NewGuid();
            var category = new Category {
                Name = "Book",
                Description = "This is a book description",
                Image = "image1"
            };

            var newProduct = new Product
            {
                Title = "Book3",
                Description = "This is a book description.",
                Price = 20,
                Images = new List<string> { "img1.jpg", "img2.jpg", "img3.jpg" },
                Inventory = 50,
                CategoryId = Guid.NewGuid()
            };

            //arrange
            _mockProductRepo.Setup(x => x.GetOneById(expectedId)).ReturnsAsync(newProduct);
            _mockCategoryRepo.Setup(x => x.GetOneById(It.IsAny<Guid>())).ReturnsAsync(new Category());
            
            //act
            var result = await _productService.GetOneById(expectedId);

            //assert
            Assert.NotNull(result);
            Assert.Equal(expectedTitle, result.Title);
            _mockProductRepo.Verify(x => x.GetOneById(expectedId), Times.Once());
        }
  
        [Fact]
        public async void GetAllItem_ValidData_ReturnListItem()
        {
            //arrange
            var ItemList = new List<Product>
            {
                new Product { Id = Guid.NewGuid(), Title = "Book3", Description = "This is a book description.", Price = 20, Images = new List<string> { "img1.jpg", "img2.jpg", "img3.jpg" }, Inventory = 50, CategoryId = Guid.NewGuid()},
                new Product { Id = Guid.NewGuid(), Title = "Book4", Description = "This is a book description.", Price = 20, Images = new List<string> { "img1.jpg", "img2.jpg", "img3.jpg" }, Inventory = 50, CategoryId = Guid.NewGuid() },
                new Product { Id = Guid.NewGuid(), Title = "Book5", Description = "This is a book description.", Price = 20, Images = new List<string> { "img1.jpg", "img2.jpg", "img3.jpg" }, Inventory = 50, CategoryId = Guid.NewGuid() },
                new Product { Id = Guid.NewGuid(), Title = "Book6", Description = "This is a book description.", Price = 20, Images = new List<string> { "img1.jpg", "img2.jpg", "img3.jpg" }, Inventory = 50, CategoryId = Guid.NewGuid() }
            };

            var queryOptions = new QueryOptionProduct
            {
                Search = string.Empty,
                Order = string.Empty,
                Offset = 0,
                Limit = 4,
                OrderByDescending = false,
                MinPrice = -1,
                MaxPrice = -1,
                CategoryId = Guid.Empty
            };

            _mockProductRepo.Setup(x => x.GetAll(queryOptions)).ReturnsAsync(ItemList);
            _mockCategoryRepo.Setup(x => x.GetOneById(It.IsAny<Guid>())).ReturnsAsync(new Category());
            
            //act
            var result = await _productService.GetAll(queryOptions);

            //assert
            Assert.NotNull(result);
            Assert.Equal(ItemList.Count, result.Count());
            _mockProductRepo.Verify(x => x.GetAll(queryOptions), Times.Once());
        }

        [Fact]
        public async void UpdateItem_ValidData_ReturnItem()
        {
            //arrange
            var productId = Guid.NewGuid();
            var updatedDto = new ProductUpdateDto { Title = "BookUpdated" };

            var foundItem = new Product { Id = Guid.NewGuid(), Title = "Book3", Description = "This is a book description.", Price = 20, Images = new List<string> { "img1.jpg", "img2.jpg", "img3.jpg" }, Inventory = 50, CategoryId = Guid.NewGuid() };

            _mockProductRepo.Setup(x => x.GetOneById(productId)).ReturnsAsync(foundItem);
            _mockProductRepo.Setup(x => x.UpdateOneById(foundItem)).ReturnsAsync(foundItem);

            var mockMapper = new Mock<IMapper>();
            mockMapper.Setup(mapper => mapper.Map(updatedDto, foundItem))
                .Callback<ProductUpdateDto, Product>((dto, entity) =>
                {
                    entity.Title = dto.Title;
                    entity.Description = dto.Description;
                    entity.Images = dto.Images;
                });

            mockMapper.Setup(mapper => mapper.Map<ProductReadDto>(foundItem)).Returns(new ProductReadDto { Title = "BookUpdated" });

            
            //act
             var result = await _productService.UpdateOneById(productId, updatedDto);
      
            //assert
            Assert.NotNull(result);
            Assert.Equal(updatedDto.Title, result.Title);
            _mockProductRepo.Verify(x => x.UpdateOneById(foundItem), Times.Once());
        }

        [Fact]
        public async Task DeleteOneById_ItemFound_ReturnsTrue()
        {
            // Arrange
            var itemId = Guid.NewGuid();
            var foundItem = new Product { Id = Guid.NewGuid(), Title = "Book3", Description = "This is a book description.", Price = 20, Images = new List<string> { "img1.jpg", "img2.jpg", "img3.jpg" }, Inventory = 50, CategoryId = Guid.NewGuid() };

            _mockProductRepo.Setup(x => x.GetOneById(itemId)).ReturnsAsync(foundItem);
            _mockProductRepo.Setup(x => x.DeleteOneById(foundItem)).Returns(Task.FromResult(true));

            // Act
            var result = await _productService.DeleteOneById(itemId);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task DeleteOneById_UserNotFound_ReturnsFalse()
        {
            // Arrange
            var itemId = Guid.NewGuid();
            _mockProductRepo.Setup(x => x.GetOneById(itemId)).ReturnsAsync((Product)null);

            // Act
            var result = await _productService.DeleteOneById(itemId);

            // Assert
            Assert.False(result);
        }
    }
}