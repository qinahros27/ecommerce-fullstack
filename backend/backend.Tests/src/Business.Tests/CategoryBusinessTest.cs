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
    public class CategoryBusinessTest
    {
        private Mock<ICategoryRepo> _mockCategoryRepo;
        private ICategoryService _categoryService;
        private IMapper _mapper;

        public CategoryBusinessTest() 
        {
            _mockCategoryRepo = new();
            _mapper = new MapperConfiguration(cfg => cfg.AddProfile<MapperProfile>()).CreateMapper();
            _categoryService = new CategoryService(_mockCategoryRepo.Object, _mapper);
        }

        [Fact]
        public async void CreateNewCategory_ValidData_ReturnNewCategory()
        {
            //arrange
            _mockCategoryRepo.Setup(x => x.CreateOne(It.IsAny<Category>())).ReturnsAsync((Category newCategory) => new Category());
            
            //act
            var newCategory = new CategoryCreateDto {
                Name = "Book",
                Description = "This is a book description",
                Image = "image1"
            };
            var createdCategory = await _categoryService.CreateOne(newCategory);

            //assert
            Assert.NotNull(createdCategory); 
            _mockCategoryRepo.Verify(x => x.CreateOne(It.IsAny<Category>()), Times.Once());
        }

        [Fact]
        public async void GetCategory_ValidData_ReturnCategory()
        {
            var expectedId = Guid.NewGuid();
            var expectedName = "Book";
        
            var Category = new Category {
                Name = "Book",
                Description = "This is a book description",
                Image = "image1"
            };
            //arrange
            _mockCategoryRepo.Setup(x => x.GetOneById(expectedId)).ReturnsAsync(Category);
            
            //act
            var result = await _categoryService.GetOneById(expectedId);

            //assert
            Assert.NotNull(result);
            Assert.Equal(expectedName, result.Name);
            _mockCategoryRepo.Verify(x => x.GetOneById(expectedId), Times.Once());
        }

        [Fact]
        public async void GetAllCategory_ValidData_ReturnListCategory()
        {
            //arrange
            var CategoryList = new List<Category>
            {
                new Category { Id = Guid.NewGuid(), Name = "Book", Description = "This is a book description", Image = "image1"},
                new Category { Id = Guid.NewGuid(), Name = "Book1", Description = "This is a book1 description", Image = "image2"},
                new Category { Id = Guid.NewGuid(), Name = "Book2", Description = "This is a book2 description", Image = "image3"},
                new Category { Id = Guid.NewGuid(), Name = "Book3", Description = "This is a book3 description", Image = "image4"}
            };

            var queryOptions = new QueryOptions
            {
                Search = string.Empty,
                Order = string.Empty,
                Offset = 0,
                Limit = 4,
                OrderByDescending = false
            };
            _mockCategoryRepo.Setup(x => x.GetAll(queryOptions)).ReturnsAsync(CategoryList);
            
            //act
            var result = await _categoryService.GetAll(queryOptions);

            //assert
            Assert.NotNull(result);
            Assert.Equal(CategoryList.Count, result.Count());
            _mockCategoryRepo.Verify(x => x.GetAll(queryOptions), Times.Once());
        }

        [Fact]
        public async void UpdateCategory_ValidData_ReturnCategory()
        {
            //arrange
            var categoryId = Guid.NewGuid();
            var updatedDto = new CategoryUpdateDto { Name = "BookUpdated" };

            var foundCategory = new Category { Id = categoryId, Name = "Book", Description = "This is a book description", Image = "image1"};

            _mockCategoryRepo.Setup(x => x.GetOneById(categoryId)).ReturnsAsync(foundCategory);
            _mockCategoryRepo.Setup(x => x.UpdateOneById(foundCategory)).ReturnsAsync(foundCategory);

            var mockMapper = new Mock<IMapper>();
            mockMapper.Setup(mapper => mapper.Map(updatedDto, foundCategory))
                .Callback<CategoryUpdateDto, Category>((dto, entity) =>
                {
                    entity.Name = dto.Name;
                    entity.Description= dto.Description;
                    entity.Image = dto.Image;
                });

            mockMapper.Setup(mapper => mapper.Map<CategoryReadDto>(foundCategory)).Returns(new CategoryReadDto { Name = "BookUpdated" });

            
            //act
             var result = await _categoryService.UpdateOneById(categoryId, updatedDto);
      
            //assert
            Assert.NotNull(result);
            Assert.Equal(updatedDto.Name, result.Name);
            _mockCategoryRepo.Verify(x => x.UpdateOneById(foundCategory), Times.Once());
        } 

        [Fact]
        public async Task DeleteOneById_CategoryFound_ReturnsTrue()
        {
            // Arrange
            var categoryId = Guid.NewGuid();
            var foundCategory = new Category { Id = categoryId, Name = "Book", Description = "This is a book description", Image = "image1"};

            _mockCategoryRepo.Setup(repo => repo.GetOneById(categoryId)).ReturnsAsync(foundCategory);
            _mockCategoryRepo.Setup(repo => repo.DeleteOneById(foundCategory)).Returns(Task.FromResult(true));

            // Act
            var result = await _categoryService.DeleteOneById(categoryId);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task DeleteOneById_CategoryNotFound_ReturnsFalse()
        {
            // Arrange
            var categoryId = Guid.NewGuid();
            _mockCategoryRepo.Setup(repo => repo.GetOneById(categoryId)).ReturnsAsync((Category)null);

            // Act
            var result = await _categoryService.DeleteOneById(categoryId);

            // Assert
            Assert.False(result);
        }
    }
}