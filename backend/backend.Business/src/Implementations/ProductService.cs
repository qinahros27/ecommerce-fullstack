using AutoMapper;
using backend.Business.src.Abstractions;
using backend.Business.src.Dtos;
using backend.Business.src.Shared;
using backend.Domain.src.Entities;
using backend.Domain.src.Abstractions;
using backend.Domain.src.Shared;

namespace backend.Business.src.Implementations
{
    public class ProductService : IProductService
    {
        private readonly IProductRepo _productRepo;
        private readonly ICategoryRepo _categoryRepo;
        protected readonly IMapper _mapper;
        public ProductService(IProductRepo productRepo,ICategoryRepo categoryRepo,  IMapper mapper) 
        {
            _productRepo = productRepo;
            _categoryRepo = categoryRepo;
            _mapper = mapper;
        }

        public async Task<ProductReadDto> CreateOne(ProductCreateDto dto)
        {
            var entity = _mapper.Map<Product>(dto);
            var created = await _productRepo.CreateOne(entity);
            created.Category = await _categoryRepo.GetOneById(entity.CategoryId);

            return new ProductReadDto
            {
                Id = created.Id,
                Title = created.Title,
                Description = created.Description,
                Price = created.Price,
                Images = created.Images,
                Category = _mapper.Map<CategoryReadDto>(created.Category) 
            };
        }

        public async Task<IEnumerable<ProductReadDto>> GetAll(QueryOptionProduct queryOptions)
        {
            var productList = await _productRepo.GetAll(queryOptions);

            foreach (var product in productList)
            {
                product.Category = await _categoryRepo.GetOneById(product.CategoryId);
            }

            return productList.Select(product => new ProductReadDto
            {
                Id = product.Id,
                Title = product.Title,
                Description = product.Description,
                Price = product.Price,
                Images = product.Images,
                Category = _mapper.Map<CategoryReadDto>(product.Category) 
            });
        }

        public async Task<ProductReadDto> GetOneById(Guid id)
        {
            var product = await _productRepo.GetOneById(id);
            var foundProduct = _mapper.Map<Product>(product);
            product.Category = await _categoryRepo.GetOneById(foundProduct.CategoryId);
            var productReadDto = _mapper.Map<ProductReadDto>(product);
            productReadDto.Category = _mapper.Map<CategoryReadDto>(product.Category);
            
            return productReadDto;
        }

        public async Task<bool> DeleteOneById(Guid id)
        {
            var foundItem = await _productRepo.GetOneById(id);
            if (foundItem is null)
            {
                return false;
            }
            await _productRepo.DeleteOneById(foundItem);
            return true;
        }

        public async Task<ProductReadDto> UpdateOneById(Guid id, ProductUpdateDto updated)
        {
            var foundItem = await _productRepo.GetOneById(id);
            if (foundItem == null)
            {
                throw new Exception("Item not found");
            }

            _mapper.Map(updated, foundItem); 
            var updatedEntity = await _productRepo.UpdateOneById(foundItem); 
            return _mapper.Map<ProductReadDto>(updatedEntity);
        }
    }
}