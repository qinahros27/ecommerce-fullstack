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
        private readonly IUserRepo _userRepo;
        private readonly IProductRepo _productRepo;
        public ReviewRateService(IReviewRateRepo reviewRateRepo,IUserRepo userRepo, IProductRepo productRepo, IMapper mapper) : base(reviewRateRepo, mapper)
        {
            _reviewRateRepo = reviewRateRepo;
            _userRepo = userRepo;
            _productRepo = productRepo;
        }
        
        public async Task<IEnumerable<ProductReviewRateReadDto>> GetAllByProduct(Guid ProductId)
        {
            var userReviewList = await _reviewRateRepo.GetAllByProduct(ProductId);
            
            foreach(var userReview in userReviewList)
            {
                userReview.User = await _userRepo.GetOneById(userReview.UserId); 
            }

            return userReviewList.Select(userReview => new ProductReviewRateReadDto
            {   
                User = _mapper.Map<UserReadDto>(userReview.User),
                Review = userReview.Review,
                RatePoint = userReview.RatePoint
            });
        }
 
        public async Task<IEnumerable<UserReviewRateReadDto>> GetAllByUser(Guid UserId)
        {
            var productReviewList = await _reviewRateRepo.GetAllByUser(UserId);
            foreach(var productReview in productReviewList)
            {
                productReview.Product = await _productRepo.GetOneById(productReview.ProductId); 
            }

            return productReviewList.Select(productReview => new UserReviewRateReadDto
            {   
                Product = _mapper.Map<ProductReadDto>(productReview.Product),
                Review = productReview.Review,
                RatePoint = productReview.RatePoint
            });
        }
    }
}