using AutoMapper;
using backend.Business.src.Abstractions;
using backend.Business.src.Dtos;
using backend.Domain.src.Entities;
using backend.Domain.src.Abstractions;
using backend.Business.src.Shared;

namespace backend.Business.src.Implementations
{
    public class ReviewRateService : BaseService<ReviewRate, ReviewRateReadDto, ReviewRateCreateDto, ReviewRateUpdateDto>, IReviewRateService
    {
        private readonly IReviewRateRepo _reviewRateRepo;
        public ReviewRateService(IReviewRateRepo reviewRateRepo, IMapper mapper) : base(reviewRateRepo, mapper)
        {
            _reviewRateRepo = reviewRateRepo;
        }
        
        public async Task<IEnumerable<ProductReviewRateReadDto>> GetAllByProduct(Guid ProductId)
        {
            var foundItem = await _reviewRateRepo.GetAllByProduct(ProductId);
            if(foundItem == null) 
            {
                throw CustomException.NotFoundException();
            }
            else 
            {
                return _mapper.Map<IEnumerable<ProductReviewRateReadDto>>(foundItem);
            }
        }
 
        public async Task<IEnumerable<UserReviewRateReadDto>> GetAllByUser(Guid UserId)
        {
            var foundItem = await _reviewRateRepo.GetAllByUser(UserId);
            if(foundItem == null) 
            {
                throw CustomException.NotFoundException();
            }
            else 
            {
                return _mapper.Map<IEnumerable<UserReviewRateReadDto>>(foundItem);
            }
        }
    }
}