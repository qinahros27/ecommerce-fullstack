using backend.Domain.src.Entities;
using backend.Domain.src.Shared;

namespace backend.Domain.src.Abstractions
{
    public interface IProductRepo
    {
        Task<IEnumerable<Product>> GetAll(QueryOptionProduct queryOptionProduct); 
        Task<Product> GetOneById(Guid id);
        Task<Product> UpdateOneById(Product updatedEntity);
        Task<bool> DeleteOneById(Product entity);
        Task<Product> CreateOne(Product entity);
    }
}