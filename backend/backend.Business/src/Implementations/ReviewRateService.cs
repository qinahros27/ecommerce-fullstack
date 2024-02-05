using AutoMapper;
using backend.Business.src.Abstractions;
using backend.Business.src.Dtos;
using backend.Domain.src.Entities;
using backend.Domain.src.Abstractions;

namespace backend.Business.src.Implementations
{
    public class ReviewRateService : BaseService<ReviewRate, ReviewRateReadDto, ReviewRateCreateDto, ReviewRateUpdateDto>, IReviewRateService
    {
        public ReviewRateService(IReviewRateRepo reviewRateRepo, IMapper mapper) : base(reviewRateRepo, mapper)
        {
        }
    }
}