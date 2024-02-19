using backend.Domain.src.Entities;

namespace backend.Domain.src.Abstractions
{
    public interface IReviewRateRepo: IBaseRepo<ReviewRate>
    {
        Task<IEnumerable<ReviewRate>> GetAllByProduct(Guid ProductId);
        Task<IEnumerable<ReviewRate>> GetAllByUser(Guid UserId);
    }
}