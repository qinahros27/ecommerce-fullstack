using Microsoft.EntityFrameworkCore;
using backend.Domain.src.Abstractions;
using backend.Domain.src.Entities;
using backend.Infrastructure.src.Database;
using backend.Business.src.Dtos;

namespace backend.Infrastructure.src.RepoImplementations
{
    public class ReviewRateRepo : BaseRepo<ReviewRate>, IReviewRateRepo
    {
        private readonly DbSet<ReviewRate> _reviewRates;
        private readonly DatabaseContext _context;
        public ReviewRateRepo(DatabaseContext dbContext) : base(dbContext)
        {
            _reviewRates = dbContext.ReviewRates;
            _context = dbContext;
        }

        public async Task<IEnumerable<ReviewRate>> GetAllByProduct(Guid productId)
        {
            return await _reviewRates.Where(rr => rr.ProductId == productId).ToListAsync();;
        }

        public async Task<IEnumerable<ReviewRate>> GetAllByUser(Guid userId)
        {
            return await _reviewRates.Where(rr => rr.UserId == userId).ToListAsync();
        }
    }
}