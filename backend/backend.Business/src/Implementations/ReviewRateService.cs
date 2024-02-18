using AutoMapper;
using backend.Business.src.Abstractions;
using backend.Business.src.Dtos;
using backend.Domain.src.Entities;
using backend.Domain.src.Abstractions;
using backend.Domain.src.Shared;

namespace backend.Business.src.Implementations
{
    public class ReviewRateService : BaseService<ReviewRate, ReviewRateReadDto, ReviewRateCreateDto, ReviewRateUpdateDto>, IReviewRateService
    {
        public ReviewRateService(IReviewRateRepo reviewRateRepo, IMapper mapper) : base(reviewRateRepo, mapper)
        {
            private readonly IBaseRepo<ReviewRate> _baseRepository;
            protected readonly IMapper _mapper;
            public override async Task<IEnumerable<ReviewRateReadDto>> GetAll(QueryOptionReviewRate queryOptions)
            {
                return _mapper.Map<IEnumerable<ReviewRateReadDto>>(await _baseRepository.GetAll(queryOptions));
            }
        }
    }
}