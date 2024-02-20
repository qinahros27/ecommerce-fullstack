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
        private readonly ICategoryRepo _categoryRepo;
        public ReviewRateService(IReviewRateRepo reviewRateRepo,IUserRepo userRepo,ICategoryRepo categoryRepo, IProductRepo productRepo, IMapper mapper) : base(reviewRateRepo, mapper)
        {
            _reviewRateRepo = reviewRateRepo;
            _userRepo = userRepo;
            _productRepo = productRepo;
            _categoryRepo = categoryRepo;
        }
        
        public async Task<IEnumerable<ProductReviewRateReadDto>> GetAllByProduct(Guid ProductId)
        {
            var userReviewList = await _reviewRateRepo.GetAllByProduct(ProductId);
            
            foreach(var userReview in userReviewList)
            {
                userReview.User = await _userRepo.GetOneById(userReview.UserId); 
            }

            return _mapper.Map<IEnumerable<ProductReviewRateReadDto>>(userReviewList);
        }
 
        public async Task<IEnumerable<UserReviewRateReadDto>> GetAllByUser(Guid UserId)
        {
            var productReviewList = await _reviewRateRepo.GetAllByUser(UserId);
            foreach(var productReview in productReviewList)
            {
                productReview.Product = await _productRepo.GetOneById(productReview.ProductId); 
                productReview.Product.Category = await _categoryRepo.GetOneById(productReview.Product.CategoryId);
            }

            return _mapper.Map<IEnumerable<UserReviewRateReadDto>>(productReviewList);
        }
    }
}