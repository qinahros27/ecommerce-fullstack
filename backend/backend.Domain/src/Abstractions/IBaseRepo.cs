using backend.Domain.src.Shared;
using System.Threading.Tasks;

namespace backend.Domain.src.Abstractions
{
    public interface IBaseRepo<T> 
    {
        Task<IEnumerable<T>> GetAll(QueryOptions queryOptions); 
        Task<T> GetOneById(Guid id);
        Task<T> UpdateOneById(T updatedEntity);
        Task<bool> DeleteOneById(T entity);
        Task<T> CreateOne(T entity);
    }
}