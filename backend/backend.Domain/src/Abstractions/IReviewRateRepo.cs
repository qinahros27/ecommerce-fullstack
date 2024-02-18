using backend.Domain.src.Entities;
using backend.Domain.src.Shared;

namespace backend.Domain.src.Abstractions
{
    public interface IReviewRateRepo: IBaseRepo<ReviewRate>
    {
        Task<IEnumerable<ReviewRate>> GetAllByProductId(QueryOptionReviewRate queryOptions); 
    }
}