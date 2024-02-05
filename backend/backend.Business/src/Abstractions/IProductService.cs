using backend.Business.src.Dtos;
using backend.Domain.src.Entities;
using backend.Domain.src.Shared;

namespace backend.Business.src.Abstractions
{
    public interface IProductService 
    {
        Task<IEnumerable<ProductReadDto>> GetAll(QueryOptionProduct queryOptionProduct);
        Task<ProductReadDto> GetOneById(Guid id);
        Task<ProductReadDto> UpdateOneById(Guid id, ProductUpdateDto updated);
        Task<bool> DeleteOneById(Guid id);
        Task<ProductReadDto> CreateOne(ProductCreateDto dto);
    }
}