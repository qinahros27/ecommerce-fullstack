using backend.Domain.src.Entities;
using backend.Business.src.Dtos;
using backend.Domain.src.Shared;

namespace backend.Business.src.Abstractions
{
    public interface IReviewRateService : IBaseService<ReviewRate, ReviewRateReadDto, ReviewRateCreateDto, ReviewRateUpdateDto>
    {
         Task<IEnumerable<ProductReviewRate>> GetAllByProductId(QueryOptionReviewRate queryOptions);
    }
}