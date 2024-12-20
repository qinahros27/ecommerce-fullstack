using backend.Domain.src.Entities;
using backend.Business.src.Dtos;

namespace backend.Business.src.Abstractions
{
    public interface IReviewRateService : IBaseService<ReviewRate, ReviewRateReadDto, ReviewRateCreateDto, ReviewRateUpdateDto>
    {
        Task<IEnumerable<ProductReviewRateReadDto>> GetAllByProduct(Guid ProductId);
        Task<IEnumerable<UserReviewRateReadDto>> GetAllByUser(Guid UserId);
    }
}